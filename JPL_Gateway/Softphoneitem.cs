using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPL_Gateway
{
    public class Softphoneitem
    {
        private bool isrun;
        private bool isselected;
        private String key;
        private String title;

        public Softphoneitem(String key, String title)
        {
            this.isselected = false;
            this.isrun = false;
            this.key = key;
            this.title = title;
        }

        public bool ISRUN
        {
            get
            {
                return isrun;
            }
            set
            {
                isrun = value;
            }
        }

        public bool ISSELECTED
        {
            get
            {
                return isselected;
            }
            set
            {
                isselected = value;
            }
        }

        public String Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
    }
}
