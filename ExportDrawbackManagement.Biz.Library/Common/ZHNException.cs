using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ExportDrawbackManagement.Biz.Library
{
    //����ZHNException��ʵ�������л�
    [Serializable]
    public sealed class ZHNException : Exception, ISerializable
    {
        //�������й�����
        public ZHNException()
            : base()
        {
        }

        public ZHNException(String message)
            : base(message)
        {
        }

        public ZHNException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
        //����һ��˽���ֶ�
        private String _ZHNProperty;
        //����һ��ֻ�����ԣ������Է����¶�����ֶ�
        public String ZHNProperty
        {
            get
            {
                return _ZHNProperty;
            }
        }

        //��д��������Message�����¶�����ֶΰ������쳣����Ϣ�ı���
        public override string Message
        {
            get
            {
                String msg = base.Message;
                //String msg = "";
                if (_ZHNProperty != null)
                {
                    msg += "ZHNException:" + _ZHNProperty;
                    //msg += Environment.NewLine + "ZHNException:" + _ZHNProperty;
                }
                return msg;
            }
        }

        //��Ϊ����������һ���µ��ֶΣ�����Ҫ����һ������Ĺ��������������л������ڸ�����һ���ܷ��࣬���Ըù������ķ������Ʊ�����Ϊ˽�з�ʽ��
        //���򣬸ù������ķ�������Ӧ�ñ�����Ϊ�ܱ�����ʽ
        private ZHNException(SerializationInfo info, StreamingContext context)
            : base(info, context)//�û��෴���л����ڶ�����ֶ�
        {
            //�����л��¶����ÿ���ֶ�
            _ZHNProperty = info.GetString("ZHNProperty");
        }

        //��Ϊ����������һ���ֶΣ�����Ҫ�������л�����
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //���л��¶����ÿ���ֶ�
            info.AddValue("ZHNProperty", _ZHNProperty);

            //�û������л����ڶ�����ֶ�
            base.GetObjectData(info, context);
        }

        //�������Ĺ����������¶�����ֶ�
        public ZHNException(String message, String _ZHNProperty)
            : this(message)//������һ��������
        {
            this._ZHNProperty = _ZHNProperty;
        }

        public ZHNException(String message, String _ZHNProperty, Exception innerException)
            : this(message, innerException)//������һ��������
        {
            this._ZHNProperty = _ZHNProperty;
        }
    }
}
