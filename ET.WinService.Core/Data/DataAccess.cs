using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ET.WinService.Core.Data
{
    public class DataAccess : IDisposable
    {
        private string _strConn;
        private bool _tranSucc = false;

        /// <summary>
        /// ʹ��Ĭ�Ϸ����������ݿ�ʵ��
        /// ������appSettings���ƽڵ�������defaultDatabase������
        /// </summary>
        public DataAccess()
        {
            SetConnectString("DataConnectionString");
        }

        /// <summary>
        /// ������ʵ����
        /// </summary>
        /// <param name="connStringName">���Ӵ����֡�����config�ļ���connectionStrings�������Ӵ���name��</param>
        public DataAccess(string connStringName)
        {
            SetConnectString(connStringName);
        }

        void SetConnectString(string connStringName)
        {
            if (ConfigurationManager.ConnectionStrings[connStringName] != null)
                this._strConn = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            else
                this._strConn = ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count - 1].ConnectionString;
        }


        ~DataAccess()
        {
            Dispose(false);
        }

        #region ExecuteScalar
        /// <summary>
        /// ִ��SQL��ѯ��䣬�����ز�ѯ�����صĽ�����е�һ�еĵ�һ�С������������к��н������ԡ� 
        /// </summary>
        /// <param name="commandText">SQL��䡣</param>
        /// <returns>������е�һ�еĵ�һ�С�</returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(CommandType.Text, commandText);
        }

        /// <summary>
        /// ִ�д�������SQL��ѯ��䣬�����ز�ѯ�����صĽ�����е�һ�еĵ�һ�С������������к��н������ԡ� 
        /// </summary>
        /// <param name="commandText">SQL��䡣</param>
        /// <param name="commandParameters">������</param>
        /// <returns>������е�һ�еĵ�һ�С�</returns>
        public object ExecuteScalar(string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ִ�д�������SQL��ѯ����洢���̣������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С������������к��н������ԡ� 
        /// </summary>
        /// <param name="commandType">��ѯ�������ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">SQL����洢�������֡�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>������е�һ�еĵ�һ�С�</returns>
        public object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (_trans == null)
                return SqlHelper.ExecuteScalar(this._strConn, commandType, commandText, commandParameters);
            else
                return SqlHelper.ExecuteScalar(_trans, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// ִ��SQL��ѯ����洢���̣������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С������������к��н������ԡ� 
        /// </summary>
        /// <param name="commandType">��ѯ�������ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">SQL����洢�������֡�</param>
        /// <returns>������е�һ�еĵ�һ�С�</returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            if (_trans == null)
                return SqlHelper.ExecuteScalar(this._strConn, commandType, commandText);
            else
                return SqlHelper.ExecuteScalar(_trans, commandType, commandText);
        }
        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// ִ��SQL��䡣 
        /// </summary>
        /// <param name="commandText">SQL��䡣</param>
        /// <returns>��Ӱ���������</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(CommandType.Text, commandText);
        }

        /// <summary>
        /// ִ�д�������SQL��䡣 
        /// </summary>
        /// <param name="commandText">SQL��䡣</param>
        /// <param name="commandParameters">������</param>
        /// <returns>��Ӱ���������</returns>
        public int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ִ��SQL����洢���̡� 
        /// </summary>
        /// <param name="commandType">��ѯ�����ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">SQL����洢�������֡�</param>
        /// <returns>��Ӱ���������</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            if (_trans == null)
                return SqlHelper.ExecuteNonQuery(_strConn, commandType, commandText);
            else
                return SqlHelper.ExecuteNonQuery(_trans, commandType, commandText);
        }

        /// <summary>
        /// ִ��SQL����洢���̡� 
        /// </summary>
        /// <param name="commandType">��ѯ�����ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">SQL����洢�������֡�</param>
        /// <returns>��Ӱ���������</returns>
        public int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(trans, cmdType, commandText, commandParameters);
        }

        /// <summary>
        /// ִ�д�������SQL����洢���̡� 
        /// </summary>
        /// <param name="commandType">��ѯ�����ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">SQL����洢�������֡�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>��Ӱ���������</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (_trans == null)
                return SqlHelper.ExecuteNonQuery(_strConn, commandType, commandText, commandParameters);
            else
                return SqlHelper.ExecuteNonQuery(_trans, commandType, commandText, commandParameters);
        }
        #endregion

        #region ExecuteReader

        /// <summary>
        /// ִ��SQL��ѯ��䣬������ SqlDataReader�� 
        /// </summary>
        /// <param name="commandText">��ѯSQL��䡣</param>
        /// <returns>һ�� SqlDataReader ����</returns>
        public SqlDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// ִ�д�������SQL��ѯ��䣬������ SqlDataReader����� 
        /// </summary>
        /// <param name="commandText">��ѯSQL��䡣</param>
        /// <param name="commandParameters">������</param>
        /// <returns>һ�� SqlDataReader ����</returns>
        public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ִ��SQL��ѯ����洢���̣������� SqlDataReader����� 
        /// </summary>
        /// <param name="commandType">��ѯ�������ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">��ѯSQL�����Ǵ洢�������֡�</param>
        /// <returns>һ�� SqlDataReader ����</returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            if (_transConn == null)
            {
                _transConn = new SqlConnection(_strConn);
                _transConn.Open();

                return SqlHelper.ExecuteReader(_transConn, commandType, commandText);
            }
            else if (_trans != null)
            {
                return SqlHelper.ExecuteReader(_trans, commandType, commandText);
            }
            else
                return SqlHelper.ExecuteReader(_transConn, commandType, commandText);
        }

        /// <summary>
        /// ִ�д�������SQL��ѯ����洢���̣������� SqlDataReader����� 
        /// </summary>
        /// <param name="commandType">��ѯ�������ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">SQL����洢�������֡�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>һ�� SqlDataReader ����</returns>
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (_transConn == null)
            {
                _transConn = new SqlConnection(_strConn);
                _transConn.Open();
                return SqlHelper.ExecuteReader(_transConn, commandType, commandText, commandParameters);
            }
            else if (_trans != null)
            {
                return SqlHelper.ExecuteReader(_trans, commandType, commandText, commandParameters);
            }
            else
                return SqlHelper.ExecuteReader(_transConn, commandType, commandText, commandParameters);

        }
        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// ִ��SQL��ѯ��䣬�����ؽ�� DataSet�� 
        /// </summary>
        /// <param name="commandText">��ѯSQL��䡣</param>
        /// <returns>һ�� DataSet ����</returns>
        public DataSet ExecuteDataSet(string commandText)
        {
            return ExecuteDataSet(CommandType.Text, commandText);
        }

        /// <summary>
        /// ִ�д�������SQL��ѯ��䣬�����ؽ�� DataSet�� 
        /// </summary>
        /// <param name="commandText">��ѯSQL��䡣</param>
        /// <param name="commandParameters">������</param>
        /// <returns>һ�� DataSet ����</returns>
        public DataSet ExecuteDataSet(string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ִ�д�������SQL��ѯ��䣬�����ؽ�� DataSet�� 
        /// </summary>
        /// <param name="commandText">��ѯSQL��䡣</param>
        /// <param name="commandParameters">������</param>
        /// <returns>һ�� DataTable ����</returns>
        public DataTable ExecuteDataTable(string commandText, params SqlParameter[] commandParameters)
        {
            DataSet ds = ExecuteDataSet(CommandType.Text, commandText, commandParameters);

            return ds.Tables[0] ?? null;
        }

        /// <summary>
        /// ִ��SQL��ѯ����洢���̣������ؽ�� DataSet�� 
        /// </summary>
        /// <param name="commandType">��ѯ�������ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">��ѯSQL�����Ǵ洢�������֡�</param>
        /// <returns>һ�� DataSet ����</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            if (_trans == null)
                return SqlHelper.ExecuteDataset(_strConn, commandType, commandText);
            else
                return SqlHelper.ExecuteDataset(_trans, commandType, commandText);
        }

        /// <summary>
        /// ִ�д�������SQL��ѯ����洢���̣������ؽ�� DataSet�� 
        /// </summary>
        /// <param name="commandType">��ѯ�������ͣ�SQL�����Ǵ洢���̡�</param>
        /// <param name="commandText">SQL����洢�������֡�</param>
        /// <param name="commandParameters">������</param>
        /// <returns>һ�� DataSet ����</returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (_trans == null)
                return SqlHelper.ExecuteDataset(_strConn, commandType, commandText, commandParameters);
            else
                return SqlHelper.ExecuteDataset(_trans, commandType, commandText, commandParameters);

        }
        #endregion

        #region Transaction
        private SqlTransaction _trans = null;
        private SqlConnection _transConn = null;

        /// <summary>
        /// �����Ƿ�ɹ��ı�ǡ�һ���ڳɹ�����֮����Ϊtrue��Ȼ�����EndTransaction()��
        /// </summary>
        public bool TransSuccess
        {
            get { return _tranSucc; }
            set { _tranSucc = value; }
        }

        /// <summary>
        /// ��ʼһ������
        /// </summary>
        public SqlTransaction BeginTransaction()
        {
            if (_transConn == null)
            {
                _transConn = new SqlConnection(_strConn);
                _transConn.Open();
            }
            if (_trans == null)
            {
                _trans = _transConn.BeginTransaction();
            }

            return _trans;
        }


        /// <summary>
        /// ������������������Ƿ�ɹ��Ĳ����Զ��ύ��ع�����
        /// </summary>
        /// <param name="isTranSucc">�����Ƿ�ɹ�</param>
        public void EndTransaction(bool isTranSucc)
        {
            if (_trans == null && _transConn == null)
                return;

            _tranSucc = isTranSucc;
            EndTransaction();

        }

        /// <summary>
        /// �������������TransSuccess���Ե�ֵ�����ύ��ع�����
        /// </summary>
        public void EndTransaction()
        {
            if (_trans == null && _transConn == null)
                return;

            try
            {
                if (_trans != null)
                {
                    if (_tranSucc)
                        _trans.Commit();
                    else
                        _trans.Rollback();
                }

            }
            finally
            {
                if (_trans != null)
                {
                    _trans.Dispose();
                    _trans = null;
                }
                if (_transConn != null)
                {
                    _transConn.Close();
                    _transConn.Dispose();
                    _transConn = null;
                }
            }
        }
        #endregion

        #region ʵ��IDisposable

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        /// <summary>
        /// �Ƿ��ͷ���Դ
        /// </summary>
        /// <param name="disposing">�ͷ���Դ��־</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //cleanup mamanged objects
                    EndTransaction();
                }
                //Cleanup unmamanged objects
            }
            _disposed = true;

        }

        #endregion

    }
}
