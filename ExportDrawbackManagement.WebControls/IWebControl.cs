namespace WebControls
{
    /// <summary>
    /// 页面栏位控件接口,所有栏位控件必须实现
    /// </summary>
    public interface IWebControl
    {
        /// <summary>
        /// 代表的栏位名
        /// </summary>
        string ControlName { get; set; }

        /// <summary>
        /// 只读
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// 验证字符串
        /// </summary>
        string RegularExpressionString { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 允许为空
        /// </summary>
        bool IsAllowNull { get; set; }

        /// <summary>
        /// 可访问
        /// </summary>
        bool ValueVisible { get; set; }

        /// <summary>
        /// 必填星显示风格
        /// </summary>
        Align StarAlign { get; set; }
    }

    /// <summary>
    /// 必填星显示风格
    /// </summary>
    public enum Align {
        /// <summary>
        /// 左
        /// </summary>
        Left, 
        /// <summary>
        /// 右
        /// </summary>
        Right,
        /// <summary>
        /// 无指定位置 默认为左
        /// </summary>
        Empty }

    /// <summary>
    /// 验证数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 无验证数据类型
        /// </summary>
        Empty,
        /// <summary>
        /// 整数
        /// </summary>
        Intege,
        /// <summary>
        /// 数字
        /// </summary>
        Num,
        /// <summary>
        /// 邮件
        /// </summary>
        Email,
        /// <summary>
        /// url
        /// </summary>
        Url,
        /// <summary>
        /// 仅中文
        /// </summary>
        Chinese,
        /// <summary>
        /// 邮编
        /// </summary>
        Zipcode,
        /// <summary>
        /// 手机
        /// </summary>
        Mobile,
        /// <summary>
        /// ip地址
        /// </summary>
        Ip4,
        /// <summary>
        /// 日期
        /// </summary>
        Date,
        /// <summary>
        /// QQ号码
        /// </summary>
        QQ,
        /// <summary>
        /// 电话号码的函数(包括验证国内区号,国际区号,分机号)
        /// </summary>
        Tel,
        /// <summary>
        /// 用来用户注册。匹配由数字、26个英文字母或者下划线组成的字符串
        /// </summary>
        UserName,
        /// <summary>
        /// 字母
        /// </summary>
        Letter,
        /// <summary>
        /// 身份证
        /// </summary>
        IdCard 
    }
}