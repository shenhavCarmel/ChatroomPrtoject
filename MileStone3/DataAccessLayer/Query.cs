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
        private String _orderBy;
        private Boolean _forMsgs;

        public Query(String from, String where, Boolean isForMsgs)
        {
            _select = "*";
            _from = from;
            _where = where;
            _forMsgs = isForMsgs;
            if (isForMsgs)
            {
                _orderBy = "SendTime DESC";
            }
            else
            {
                _orderBy = "Id";
            }
        }

        public Query()
        {

        }

        public void setOrderBy(String orderBy, Boolean isDesc)
        {    
            if (isDesc)
            {
                _orderBy = orderBy + " DESC";
            }
            else
            {
                _orderBy = orderBy;
            }         
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

        public String getWhere()
        {
            return _where;
        }
        public String toString()
        {
            if (_where.Equals(""))
            {
                if (_forMsgs)
                    return "select top 200 " + _select + " from " + _from + " order by "+_orderBy;
                return "select " + _select + " from " + _from + " order by " + _orderBy;
            }

            else
            {
                if (_forMsgs)
                    return "select top 200 " + _select + " from " + _from + " where " + _where + " order by " + _orderBy;
                return "select " + _select + " from " + _from + " where " + _where + " order by " + _orderBy;
            }
        }
    }
}
