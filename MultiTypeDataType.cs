using System;
using System.Collections.Generic;

using _4Ben.DataTypes.MultiType.Helpers.Data;

using umbraco.cms.businesslogic.datatype;

namespace _4Ben.DataTypes.MultiType
{
    public class MultiTypeDataType : AbstractDataEditor
    {

        private umbraco.interfaces.IData _xmlData;
        private umbraco.interfaces.IDataEditor _multiTypeDataEditor;
        private umbraco.interfaces.IDataPrevalue _multiTypePrevalueEditor;

        public override string DataTypeName
        {
            get { return "MultiType"; }
        }

        public override Guid Id
        {
            get { return new Guid("818EFF55-5C36-4633-AC0D-5E5F357C3290"); }
        }

        public override umbraco.interfaces.IData Data
        {

            get
            {
                if (_xmlData == null)
                    _xmlData = new XmlData(this);

                return _xmlData;
            }
        }

        public override umbraco.interfaces.IDataEditor DataEditor
        {
            get
            {
                if (_multiTypeDataEditor == null)
                    _multiTypeDataEditor = new MultiTypeDataEditor(Data, ((MultiTypePrevalueEditor)this.PrevalueEditor).GetPreValueOptions<MultiTypeOptions>());
                return _multiTypeDataEditor;
            }
        }

        public override umbraco.interfaces.IDataPrevalue PrevalueEditor
        {
            get
            {
                if (_multiTypePrevalueEditor == null)
                    _multiTypePrevalueEditor = new MultiTypePrevalueEditor(this);
                return _multiTypePrevalueEditor;
            }
        }

    }
}