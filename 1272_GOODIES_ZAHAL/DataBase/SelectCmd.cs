using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1272_GOODIES_ZAHAL.DataBase
{
    public class SelectCmd : DbCmd
    {
        protected string m_keyVal;



        /// <summary>
        /// Init the members
        /// </summary>
        protected override void Init()
        {
            m_keyVal = null;
        }

        /// <summary>
        /// Add text key value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddTextKeyVal(string clmName, string val)
        {
            AddKeyVal(clmName, val);
        }

        /// <summary>
        /// Add bool key value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddBoolKeyVal(string clmName, bool val)
        {
            AddKeyVal(clmName, GetBoolVal(val));
        }

        /// <summary>
        /// Add int key value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddIntKeyVal(string clmName, int val)
        {
            AddKeyVal(clmName, val.ToString());
        }

        /// <summary>
        /// Add  key value 
        /// </summary>
        /// <param name="clmName">the coulm name</param>
        /// <param name="val">the value</param>
        public void AddKeyVal<T>(string clmName, T val)
        {
            string curVal = $"{clmName}  = '{val}'";
            if (m_keyVal != null)
            {
                m_keyVal += " AND ";
            }
            m_keyVal += curVal;
        }



        /// <summary>
        /// Get the command
        /// </summary>
        /// <returns>the command</returns>
        public override string GetCmd()
        {
            string cmd = null;
            if (m_keyVal != null)
            {
                cmd = $"SELECT * FROM {m_tblName} WHERE {m_keyVal} ";
            }
            else
            {
                cmd = $"SELECT * FROM {m_tblName}";
            }
            Init();
            return cmd;
        }

    }
}
