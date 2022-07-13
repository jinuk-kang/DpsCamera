using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpsCamera {
    internal class ParseManager {
        public static string parseBarcode(String input) {
            Console.WriteLine(input.Substring(1, input.Length - 2));
            return input.Substring(1, input.Length - 2);
        }
    }
}
