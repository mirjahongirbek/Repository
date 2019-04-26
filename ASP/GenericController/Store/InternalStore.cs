using RepositoryRule.Attributes;
using RepositoryRule.State;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace GenericController.Store
{
    internal class InternalStore
    {
        public static List<ModalList> Modals { get; set; }
        public static Dictionary<string, PropsAttribute> ParseModalProps(ModalList modal, Type tip)
        {
            Dictionary<string, PropsAttribute> result = new Dictionary<string, PropsAttribute>();
            foreach (var i in modal.ViewModal.GetProperties())
            {
               var attr = i.GetCustomAttribute<PropsAttribute>();
                if(attr== null)
                {
                   var props= tip.GetProperties().FirstOrDefault(m=>m.Name.ToLower()== i.Name.ToLower());
                    if (props != null) 
                    result.Add(Char.ToLower(i.Name[0]) + i.Name.Substring(1), props.GetProps());
                }else
                result.Add(Char.ToLower(i.Name[0]) + i.Name.Substring(1), attr);
            }
            return result;
        }
    }
}
