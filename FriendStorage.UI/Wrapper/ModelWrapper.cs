using FriendStorage.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FriendStorage.UI.Wrapper
{
    public class ModelWrapper<T> : Observable, IRevertibleChangeTracking
    {
        private Dictionary<string, object> _orignalValues;

        private List<IRevertibleChangeTracking> _trackingObjects;
        public T Model { get; set; }

        public ModelWrapper(T model)
        {
            // if null throw new ArgumentNullException("model");
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            Model = model;// ?? throw new ArgumentNullException("model");

            _orignalValues = new Dictionary<string, object>();
            _trackingObjects = new List<IRevertibleChangeTracking>();
        }

        protected void SetValue<Tvalue>(Tvalue newValue, [CallerMemberName]string propertyName = null)
        {

            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Model);

            if (!Equals(currentValue, newValue))
            {
                UpdateOriginalValue(currentValue, newValue, propertyName); // First save original value after setValue
                propertyInfo.SetValue(Model, newValue);
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName + "IsChanged");
            }

        }

        private void UpdateOriginalValue(object currentValue, object newValue, string propertyName)
        {
            if (!_orignalValues.ContainsKey(propertyName))
            {
                _orignalValues.Add(propertyName, currentValue);
                OnPropertyChanged(nameof(IsChanged));
            }
            else
            {
                if (Equals(_orignalValues[propertyName], newValue))
                {
                    _orignalValues.Remove(propertyName);
                    OnPropertyChanged(nameof(IsChanged));
                }
            }
        }
        public bool IsChanged { get { return _orignalValues.Count > 0 ||_trackingObjects.Any(t=> t.IsChanged); } }

        public void AcceptChanges()
        {
            _orignalValues.Clear();
            foreach (var trackingObject in _trackingObjects   )
            {
                trackingObject.AcceptChanges();
            }

            OnPropertyChanged("");// all propertes will refresh 
        }

        public void RejectChanges()
        {
            foreach (var originalValueEntry in _orignalValues)
            {
                //  SetValue(originalValueEntry.Value, originalValueEntry.Key);
                //reflection is here
                typeof(T).GetProperty(originalValueEntry.Key).SetValue(Model, originalValueEntry.Value);
            }
            _orignalValues.Clear();

            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.RejectChanges();
            }

            OnPropertyChanged("");// all propertes will refresh 
        }
        protected Tvalue GetValue<Tvalue>([CallerMemberName]string propertyName = null)
        {

            var propertyInfo = Model.GetType().GetProperty(propertyName);
            return (Tvalue)propertyInfo.GetValue(Model);

        }



        protected Tvalue GetOriginalValue<Tvalue>(string propertyName)
        {
            return _orignalValues.ContainsKey(propertyName)
                ? (Tvalue)_orignalValues[propertyName]
                : GetValue<Tvalue>(propertyName);

        }

        protected bool GetIsChanged(string propertyName)
        {
            return _orignalValues.ContainsKey(propertyName);
        }

        protected void RegisterCollection<TWrapper, TModel>(
           ChangeTrackingCollections<TWrapper> wrapperCollection,
           List<TModel> modelCollection) where TWrapper : ModelWrapper<TModel>
        {
            wrapperCollection.CollectionChanged +=
               (s, e) =>
               {
                   if (e.OldItems != null)
                   {
                       foreach (var item in e.OldItems.Cast<TWrapper>())
                       {
                           modelCollection.Remove(item.Model);
                       }
                   }

                   if (e.NewItems != null)
                   {
                       foreach (var item in e.NewItems.Cast<TWrapper>())
                       {
                           modelCollection.Add(item.Model);
                       }
                   }
               };
            RegisterTrackingObject(wrapperCollection);
        }


        protected void RegisterComplex<TModel>(ModelWrapper<TModel> wrapper)
        {
            RegisterTrackingObject(wrapper);
        }

        private void RegisterTrackingObject<TTrackingObject>(TTrackingObject trackingObject)
            where TTrackingObject :IRevertibleChangeTracking,INotifyPropertyChanged
        {
            if (!_trackingObjects.Contains(trackingObject))
            {
                _trackingObjects.Add(trackingObject);
                trackingObject.PropertyChanged += TrackingObjectPropertyChanged;
            }
        }

        private void TrackingObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        { if(e.PropertyName == nameof(IsChanged))
            {
                OnPropertyChanged(nameof(IsChanged));
            }
        }
    }
}
