using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone3.DataAccessLayer
{
    public class Query
    {
        private String _select;
        private String _from;
        private String _where;

        public Query( String from, String where)
        {
            _select = "*";
            _from = from;
            _where = where;
        }

        public Query()
        {

        }

        public void setSelect(String input)
        {
            _select = input;
        }

        public void setFrom(String input)
        {
            _from = input;
        }

        public void setWhere(String input)
        {
            _where = input;
        }
    
        public String toString()
        {
            if (_where.Equals(""))
            {
                return "select " + _select + " from " + _from;
            }
            else
            {
                return "select " + _select + " from " + _from + " where " + _where;
            }
        }
    }
}