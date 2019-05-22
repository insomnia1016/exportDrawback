using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace ExportDrawbackManagement.Biz.Library
{
    //允许ZHNException的实例被序列化
    [Serializable]
    public sealed class ZHNException : Exception, ISerializable
    {
        //三个公有构造器
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
        //定义一个私有字段
        private String _ZHNProperty;
        //定义一个只读属性，该属性返回新定义的字段
        public String ZHNProperty
        {
            get
            {
                return _ZHNProperty;
            }
        }

        //重写公有属性Message，将新定义的字段包含在异常的消息文本中
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

        //因为定义了至少一个新的字段，所以要定义一个特殊的构造器用作反序列化。由于该类是一个密封类，所以该构造器的访问限制被定义为私有方式。
        //否则，该构造器的访问限制应该被定义为受保护方式
        private ZHNException(SerializationInfo info, StreamingContext context)
            : base(info, context)//让基类反序列化其内定义的字段
        {
            //反序列化新定义的每个字段
            _ZHNProperty = info.GetString("ZHNProperty");
        }

        //因为定义了至少一个字段，所以要定义序列化方法
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //序列化新定义的每个字段
            info.AddValue("ZHNProperty", _ZHNProperty);

            //让基类序列化其内定义的字段
            base.GetObjectData(info, context);
        }

        //定义额外的构造器设置新定义的字段
        public ZHNException(String message, String _ZHNProperty)
            : this(message)//调用另一个构造器
        {
            this._ZHNProperty = _ZHNProperty;
        }

        public ZHNException(String message, String _ZHNProperty, Exception innerException)
            : this(message, innerException)//调用另一个构造器
        {
            this._ZHNProperty = _ZHNProperty;
        }
    }
}
