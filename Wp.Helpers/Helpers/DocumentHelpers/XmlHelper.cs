using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Interfaces;
using static Wp.Helpers.Helpers.PropertyHepler;

namespace Wp.Helpers.Helpers.DocumentHelpers
{
    public class XmlHelper
    {
        public static void SaveAllElement(string path, IXmlModel model)
        {
            PropertyValue<IXmlModel> propertyValue = new PropertyValue<IXmlModel>(model);
            var res = new Dictionary<string, string>();
            var t = model.GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                if (p.GetType() == typeof(ICollection<IXmlModel>))
                {
                    var v = propertyValue.Get(p.Name) as ICollection<IXmlModel>;
                    foreach (var item in v)
                    {
                    }
                }
                else
                {
                    var v = propertyValue.Get(p.Name);
                    res.Add(p.Name, v is null ? string.Empty : v.ToString());
                }
            }

            //if (ExceptType != null)
            //{
            //}
            //else
            //{
            //    foreach (PropertyInfo p in typeof(T).GetProperties(bindingFlags))
            //    {
            //        var v = propertyValue.Get(p.Name);
            //        res.Add(p.Name, v is null ? string.Empty : v.ToString());
            //    }
            //}
            //return res;
        }

        public static void GetAllElement()
        {
        }

        public static void DeleteOneElement()
        {
        }
    }
}