using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SaveTesting {

    class Test {
        private string field0 = "secret";
        public decimal field1 = 1.25m;
        public string field2  = "Hello";

        public string[] field3  = { "Dogs", "Cats", "Birds" };
        public List<int> field4 = new List<int>(new int[]{ 1, 2, 3, 4 });
        public Test field5;

        public string prop1 { get; set; }
        public string prop2 { get; private set; }
        public int prop3 { get; set; }

        public Test() {
            prop1 = "First property";
            prop2 = "Second Property";
            prop3 = 5;
        }
    }


    class Program {
        static void Main(string[] args) {
            string[] blacklist = {
                "field3",
                "field4",
                "field5"
            };

            var test = new Test();
            var fields = getAllFields<Test>(test, blacklist);

            foreach (var field in fields) {
                Console.WriteLine(field);
            }

            Console.Read();
        }

        public static List<string> getAllFields<T>(object obj, string[] blacklist) {
            var lst = new List<string>();

            var fields = typeof(T).GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance
            );

            for (int x = 0; x < fields.Length; x++) {
                var field = fields[x];

                if (!blacklist.Contains(field.Name)) {
                    lst.Add(field.GetValue(obj).ToString());
                }
            }

            return lst;
        }
    }
}
