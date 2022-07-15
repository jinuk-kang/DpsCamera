using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpsCamera {
    internal class ParseManager {
        public static String parseBarcode(String input) {
            Console.WriteLine(input.Substring(1, input.Length - 2));
            return "aa";
            return input.Substring(1, input.Length - 2);
        }
        public static String parseRound(String barcode) {
            return "bb";
            return barcode.Substring(0, 11);
        }
        public static String parseStoreCode(String barcode) {
            return "cc";
            return barcode.Substring(11, 5);
        }
        public static String parseBoxOrder(String barcode) {
            return "dd";
            return barcode.Substring(16, 2);
        }
        public static String parseDivergence(String barcode) {
            return "ee";
            return barcode.Substring(19, 1);
        }
    }
}
