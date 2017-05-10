/* 基础信息 */
export const SET_USER_INFO = 'SET_USER_INFO' // 设置用户信息
export const SET_COM_INFO = 'SET_COM_INFO' // 公司信息
    /* IM 新 */
export const IM_ON_MSG_NOTIFY = 'IM_ON_MSG_NOTIFY' // 新消息
export const IM_CUT_FRIEND_GROUP = 'IM_CUT_FRIEND_GROUP' // 新消息
    /* IM系统 旧 */
export const SET_FRIEND_LIST = 'SET_FRIEND_LIST' // 设置好友列表
export const SET_GROUP_LIST = 'SET_GROUP_LIST' // 设置群组列表
export const SET_RECENT_CONTACTS = 'SET_RECENT_CONTACTS' // 设置最近联系人
export const SET_SYS_MESS = 'SET_SYS_MESS' // 通知公告等
export const SET_IM_STICK = 'SET_IM_STICK' // 消息置顶
    /* work */
    // 状态类
export const WORK_POPUP_AUDIT = 'WORK_POPUP_AUDIT' // 审核弹窗
export const WORK_POPUP_ERROR = 'WORK_POPUP_ERROR' // 错误弹窗
export const WORK_POPUP_SAVE = 'WORK_POPUP_SAVE' // 保存弹窗
export const WORK_POPUP_AFFIRM = 'WORK_POPUP_AFFIRM' // 审核的确认弹窗
    // 一些基本下拉数据
export const WORK_APPLY_USER = 'WORK_APPLY_USER' // 应用用户列表
export const WORK_PRODUCT_CLASS = 'WORK_PRODUCT_CLASS' // 产品类别（商品类型列表）
export const WORK_SUPPLIER_LIST = 'WORK_SUPPLIER_LIST' // 获取供应商列表
export const WORK_COUNTTER_LIST = 'WORK_COUNTTER_LIST' // 获取柜组列表
export const WORK_APPLY_DEPARTEMENT = 'WORK_APPLY_DEPARTEMENT' // 计划分销商
export const WORK_REPOSITORY_LIST = 'WORK_REPOSITORY_LIST' // 计划分销商
    // 核心操作
export const WORK_USER_TYPE = 'WORK_USER_TYPE' // 监听编辑权限
export const WORK_PRODUCT_STATUS = 'WORK_PRODUCT_STATUS' // 监听编辑权限
export const DELECT_SELECTS = 'DELECT_SELECTS' // 删除数据
export const SEEK_BARCODE = 'SEEK_BARCODE' // 通过条形码搜索数据
export const WORK_PRODUCT_DETAIL = 'WORK_PRODUCT_DETAIL' // 产品的详细内容
export const WORK_DELECT_SELECTS = 'WORK_DELECT_SELECTS' // 删除数据的集合（公共方法）
export const WORK_RECEIPT_DETAIL = 'WORK_RECEIPT_DETAIL' // 单据详情
export const WORK_PRODUCT_LIST = 'WORK_PRODUCT_LIST' // 单据列表（公共数据）
export const WORK_DEPARTMENT_LIST = 'WORK_DEPARTMENT_LIST' // 获取操作部门（含店铺）
export const WORK_ONLY_DEPARTMENT = 'WORK_ONLY_DEPARTMENT' // 只有操作部门 待删
export const WORK_ONLY_SHOP = 'WORK_ONLY_SHOP' // 只有店铺 待删
export const WORK_SHOP_LIST = 'WORK_SHOP_LIST' // 店铺列表（分销商）待删
export const WORK_SHOP_LIST_BY_CO = 'WORK_SHOP_LIST_BY_CO' // 店铺列表
export const WORK_STORAGE_LIST = 'WORK_STORAGE_LIST' // 商品列表（销售）
export const SEEK_GOODS_LIST = 'SEEK_GOODS_LIST' // 商品列表（销售除外）
export const DELECT_PRODUCT_DETAIL = 'DELECT_PRODUCT_DETAIL' // 商品操作（删）
export const RECEIPT_ADD_OR_CHECK = 'RECEIPT_ADD_OR_CHECK' // 单据新增/提交审核/保存（销售除外）
    // export const STORAGE_PULL _DOWM = 'STORAGE_PULL _DOWM' // 下拉数据（商品大小类列表）
    // 销售
export const SELL_PRODUCT_LIST = 'SELL_PRODUCT_LIST' // 商品列表-销售
export const SELL_PRODUCT_LIST_DISCOUNT = 'SELL_PRODUCT_LIST_DISCOUNT' // 商品列表-销售-折扣
export const SELL_PRODUCT_LIST_NEWPRICE = 'SELL_PRODUCT_LIST_NEWPRICE' // 商品列表-销售-实售价
export const SELL_PRODUCT_LIST_SALEGOLDPRICE = 'SELL_PRODUCT_LIST_SALEGOLDPRICE' // 商品列表-销售-销售金价
export const SELL_PRODUCT_LIST_PAYMENTPRICE = 'SELL_PRODUCT_LIST_PAYMENTPRICE' // 商品列表-销售-手工费
export const SELL_PRODUCT_LIST_CALCMETHOD = 'SELL_PRODUCT_LIST_CALCMETHOD' // 商品列表-销售-计价
export const SELL_PRODUCT_LIST_OLDPRICE = 'SELL_PRODUCT_LIST_OLDPRICE' // 销售-原售价
    // 销售-----退还
export const SELL_PRODUCT_LIST_EX_EXCHANGEGOLDPRICE = 'SELL_PRODUCT_LIST_EX_EXCHANGEGOLDPRICE' // 退换-回收金价
export const SELL_PRODUCT_LIST_EX_ABRASION = 'SELL_PRODUCT_LIST_EX_ABRASION' // 退换-损耗
export const SELL_PRODUCT_LIST_EX_ESTIMATEPRICE = 'SELL_PRODUCT_LIST_EX_ESTIMATEPRICE' // 退换-估价
export const SELL_PRODUCT_LIST_EX_PAYMENTPRICE = 'SELL_PRODUCT_LIST_EX_PAYMENTPRICE' // 退换-手工费
export const SELL_PRODUCT_LIST_EX_NEWPRICE = 'SELL_PRODUCT_LIST_EX_NEWPRICE' // 商品列表-退换-实售价
export const SELL_PRODUCT_LIST_EX_CALCMETHOD = 'SELL_PRODUCT_LIST_EX_CALCMETHOD' // 商品列表-退换-计价
    // 销售------回收
export const SELL_PRODUCT_LIST_RE_RECYCLEGOLDPRICE = 'SELL_PRODUCT_LIST_RE_RECYCLEGOLDPRICE' // 回收-回收金价
export const SELL_PRODUCT_LIST_RE_ABRASION = 'SELL_PRODUCT_LIST_RE_ABRASION' // 回收-损耗
export const SELL_PRODUCT_LIST_RE_ESTIMATEPRICE = 'SELL_PRODUCT_LIST_RE_ESTIMATEPRICE' // 回收-估价
export const SELL_PRODUCT_LIST_RE_PAYMENTPRICE = 'SELL_PRODUCT_LIST_RE_PAYMENTPRICE' // 回收-手工费
export const SELL_PRODUCT_LIST_RE_NEWPRICE = 'SELL_PRODUCT_LIST_RE_NEWPRICE' // 商品列表-回收-实售价
export const SELL_PRODUCT_LIST_RE_CALCMETHOD = 'SELL_PRODUCT_LIST_RE_CALCMETHOD' // 商品列表-回收-计价

// 打印模板
export const RECEIVE_ERROR = 'RECEIVE_ERROR'

export const RECEIVE_LABEL_LIST = 'RECEIVE_LABEL_LIST'
export const RECEIVE_QUALITY_LIST = 'RECEIVE_QUALITY_LIST'
export const QUALITY_TEMPLATE_LIST_APPEND = 'QUALITY_TEMPLATE_LIST_APPEND'
export const LABEL_TEMPLATE_LIST_APPEND = 'LABEL_TEMPLATE_LIST_APPEND'
export const QUALITY_TEMPLATE_LIST_REMOVE = 'QUALITY_TEMPLATE_LIST_REMOVE'
export const LABEL_TEMPLATE_LIST_REMOVE = 'LABEL_TEMPLATE_LIST_REMOVE'
export const QUALITY_TEMPLATE_LIST_UPDATED = 'QUALITY_TEMPLATE_LIST_UPDATED'
export const LABEL_TEMPLATE_LIST_UPDATED = 'LABEL_TEMPLATE_LIST_UPDATED'
export const EMPTY_TEMPLATE_LIST = 'EMPTY_TEMPLATE_LIST'
export const RECEIVE_SHOP_LIST = 'RECEIVE_SHOP_LIST'
export const RECEIVE_TEMPLATE_SIZE_LIST = 'RECEIVE_TEMPLATE_SIZE_LIST'
export const RECEIVE_PRODUCT_PROP_LIST = 'RECEIVE_PRODUCT_PROP_LIST'
export const RECEIVE_TEMPLATE_DETAIL = 'RECEIVE_TEMPLATE_DETAIL'
export const TEMPLATE_DETAIL_UPDATED = 'TEMPLATE_DETAIL_UPDATED'