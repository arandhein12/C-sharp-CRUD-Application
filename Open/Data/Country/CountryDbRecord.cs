using System;
using Open.Core;

namespace Open.Data.Country {

    public class CountryDbRecord : RootObject {

        private string id;
        private string code;
        private string name;
        private DateTime validFrom = DateTime.MinValue;
        private DateTime validTo= DateTime.MaxValue;

        public string ID {
            get => getValue(ref id, Name);
            set => id = value;
        }

        public string Code {
            get => getValue(ref code, Constants.Unspecified);
            set => code = value;
        }

        public string Name {
            get => getValue(ref name, Code);
            set => name = value;
        }

        public DateTime ValidFrom {
            get => getMinValue(ref validFrom, ref validTo);
            set => validFrom = value;
        }

        public DateTime ValidTo {
            get => getMaxValue(ref validTo, ref validFrom);
            set => validTo = value;
        }
    }
}
        
