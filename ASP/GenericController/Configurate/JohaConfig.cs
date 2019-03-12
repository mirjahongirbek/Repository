
using System.Collections.Generic;

namespace GenericController.Configurate
{
    public class JohaConfig
    {
        #region
        
        #endregion
        private JohaConfig()
        {

        }
        public static JohaConfig Init(string name)
        {
            return new JohaConfig();
        }
        public void AddAuth()
        {

        }
        public JohaConfig AddRols(Dictionary<string, string[]> methodNames)
        {

            return this;
        }

    }
}
