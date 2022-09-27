using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DpsCamera {
    public class BarcodeInfo {
        public string dateString;
        public string barcode;
        public string round;
        public string storeCode;
        public string boxOrder;
        public string divergence;

        public BarcodeInfo(string dateString, string barcode, string round, string storeCode, string boxOrder, string divergence) {
            this.dateString = dateString;
            this.barcode = barcode;
            this.round = round;
            this.storeCode = storeCode;
            this.boxOrder = boxOrder;
            this.divergence = divergence;
        }
    }
}
