using FriendStorage.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FriendStorage.UI.Wrapper
{
    public class ModelWrapper<T>:Observable
    {

        public T Model { get; set; }

        public ModelWrapper(T model)
        {
            // if null throw new ArgumentNullException("model");
            if(model == null)
            {
                throw new ArgumentNullException("model");
            }
            Model = model;// ?? throw new ArgumentNullException("model");
        }

        protected void SetValue<Tvalue>(Tvalue value, [CallerMemberName]string propertyName = null)
        {

            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Model);

            if (!Equals(currentValue, value))
            {
                propertyInfo.SetValue(Model, value);
                OnPropertyChanged(propertyName);
            }

        }

        protected Tvalue GetValue<Tvalue>([CallerMemberName]string propertyName = null)
        {

            var propertyInfo = Model.GetType().GetProperty(propertyName);
            return (Tvalue)propertyInfo.GetValue(Model);
            
        }

        protected void RegisterCollection<TWrapper, TModel>(
           ObservableCollection<TWrapper> wrapperCollection,
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
        }

    }
}
