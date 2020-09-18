using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePrismProperties
{
    public class PropertyModel : BindableBase
    {
        /// <summary>
        /// The type of a property
        /// </summary>
        private string _propertyType;

        public string PropertyType
        {
            get => _propertyType;
            set => SetProperty(ref _propertyType, value);
        }

        /// <summary>
        /// The name of a property 
        /// </summary>
        private string _propertyName;

        public string PropertyName
        {
            get => _propertyName;
            set => SetProperty(ref _propertyName, value);
        }

        /// <summary>
        /// The discription of a property
        /// </summary>
        private string _discription;

        public string Discription
        {
            get => _discription;
            set => SetProperty(ref _discription, value);
        }


        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PropertyModel()
        {

        }

        #endregion
    }
}
