using System;
using System.Collections.Generic;
using System.Text;
using PLM.Framework.Common;

namespace PLM.Biz.Interface
{
    public partial class Q_LocalPerformM
    {
        private QueryType _queryType;
        /// <summary>
        /// �۸�ˮƽ��ѯ����
        /// </summary>
        public QueryType QueryType
        { get { return _queryType; } set { _queryType = value; } }

        private String _trafMode;
        /// <summary>
        /// ���䷽ʽ��A-�������䷽ʽ��5-���ˡ�
        /// </summary>
        public String TrafMode
        { get { return _trafMode; } set { _trafMode = value; } }
    }

    public partial class Q_LocalPerformQ
    {
        private QueryType _queryType;
        /// <summary>
        /// �۸�ˮƽ��ѯ����
        /// </summary>
        public QueryType QueryType
        { get { return _queryType; } set { _queryType = value; } }

        private String _trafMode;
        /// <summary>
        /// ���䷽ʽ��A-�������䷽ʽ��5-���ˡ�
        /// </summary>
        public String TrafMode
        { get { return _trafMode; } set { _trafMode = value; } }
    }

    public partial class Q_LocalPerformY
    {
        private QueryType _queryType;
        /// <summary>
        /// �۸�ˮƽ��ѯ����
        /// </summary>
        public QueryType QueryType
        { get { return _queryType; } set { _queryType = value; } }

        private String _trafMode;
        /// <summary>
        /// ���䷽ʽ��A-�������䷽ʽ��5-���ˡ�
        /// </summary>
        public String TrafMode
        { get { return _trafMode; } set { _trafMode = value; } }
    }

    /// <summary>
    /// �۸�ˮƽ��ѯ����
    /// </summary>
    public enum QueryType
    {
        [Remark("һ��ó����Ʒ�۸�ˮƽ")]
        һ��ó����Ʒ�۸�ˮƽ = 0,
        [Remark("�ص�˰Դ��Ʒ�۸�ˮƽ")]
        �ص�˰Դ��Ʒ�۸�ˮƽ = 1,
        [Remark("�����ϼ������۸�ˮƽ")]
        �����ϼ������۸�ˮƽ = 2,
        [Remark("�����ϼ������۸�ˮƽ")]
        �����ϼ������۸�ˮƽ = 3,
        [Remark("�����ϼ������۸�ˮƽ")]
        �����ϼ������۸�ˮƽ = 4,
    }

    public enum Modi_Flag
    {
        [Remark("�ɽ��ܼ�")]
        P,
        [Remark("����")]
        Q,
        [Remark("˰��")]
        C,
    }
}
