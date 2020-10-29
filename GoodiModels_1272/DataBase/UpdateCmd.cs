using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodiModels_1272.DataBase
{
    public class UpdateCmd : SelectCmd
    {
        string m_updatedVal;



        /// <summary>
        /// Init the members
        /// </summary>
        protected override void Init()
        {
            m_updatedVal = null;
            m_keyVal = null;
        }

        /// <summary>
        /// Add text value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddTextVal(string clmName, string val)
        {
            AddVal(clmName, GetTextVal(val));
        }


        public void AddDateTimeVal(string clmName, string val)
        {
            AddVal(clmName, val);
        }

        /// <summary>
        /// Add bool value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddBoolVal(string clmName, bool val)
        {
            AddVal(clmName, GetBoolVal(val));
        }

        /// <summary>
        /// Add int value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddIntVal(string clmName, int val)
        {
            AddVal(clmName, val.ToString());
        }



        /// <summary>
        /// Add  value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddVal<T>(string clmName, T val)
        {
            AddVal(ref m_updatedVal, clmName, val);
        }

        /// <summary>
        /// Add double value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddDoubleVal(string clmName, double val)
        {
            AddVal(clmName, val.ToString());
        }

        /// <summary>
        /// Add null value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddNullVal(string clmName)
        {
            AddVal(clmName, "NULL");
        }

        /// <summary>
        /// add value
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        private void AddVal<T>(ref string srcVal, string clmName, T val)
        {
            string curVal = clmName + " = " + val;
            if (srcVal != null)
            {
                srcVal += ",";
            }
            srcVal += curVal;
        }

        /// <summary>
        /// Get the command
        /// </summary>
        /// <returns>the command</returns>
        public override string GetCmd()
        {
            string cmd = "";
            if (m_keyVal != null)
            {
                cmd = $"UPDATE {m_tblName} SET {m_updatedVal} WHERE {m_keyVal}";
            }
            else
            {
                cmd = $"UPDATE {m_tblName} SET {m_updatedVal}";
            }
            Init();
            return cmd;
        }
    }
}