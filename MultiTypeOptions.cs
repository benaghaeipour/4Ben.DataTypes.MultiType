using System;
using System.Collections.Generic;

using _4Ben.DataTypes.MultiType.Helpers.PrevalueEditors;

namespace _4Ben.DataTypes.MultiType
{
    public class MultiTypeOptions : AbstractOptions
    {
        public int Limit { get; set; }
        public int MacroId { get; set; }
        public List<MultiType> MultiTypes { get; set; }
        
        public MultiTypeOptions()
        {
        }

        public MultiTypeOptions(bool loadDefaults)
            : base(loadDefaults)
        {
        }

        public MultiTypeOptions GetOptions()
        {
            var options = this;
            if (options == null)
            {
                options = new MultiTypeOptions(true);
            }
            if (options.MultiTypes == null)
            {
                options.MultiTypes = new List<MultiType>();
                options.Limit = 0;
            }

            return options;
        }
    }
}