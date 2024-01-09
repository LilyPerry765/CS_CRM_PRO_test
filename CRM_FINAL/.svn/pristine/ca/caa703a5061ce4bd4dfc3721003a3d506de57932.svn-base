using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CRM.CodeGenerator.Data
{
    public class Table : IEnumerable<Column>
    {
        private List<Column> _Columns;

        public int ID { get; set; }
        public string Name { get; set; }

        public Table()
        {
            _Columns = new List<Column>();
        }

        public List<Column> Columns
        {
            get
            {
                return _Columns;
            }
        }

        public IEnumerator<Column> GetEnumerator()
        {
            foreach (var column in _Columns)
            {
                yield return column;
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var column in _Columns)
            {
                yield return column;
            }
        }
    }
}
