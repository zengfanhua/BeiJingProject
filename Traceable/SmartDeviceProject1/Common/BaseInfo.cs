namespace SmartDeviceProject1.Common
{
    public abstract class BaseInfo
    {
        /// <summary>
        ///     物料号 Matnr
        /// </summary>
        public string Matnr { get; set; }

        /// <summary>
        ///    供应商批号
        /// </summary>
        public string Zvlot { get; set; }
        
        /// <summary>
        ///    批次
        /// </summary>
        public string Charg { get; set; }

        /// <summary>
        ///     基本计量单位
        /// </summary>
        public string Meins { get; set; }

        /// <summary>
        ///     包号
        /// </summary>
        public string Zpnum { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        public decimal Menge { get; set; }

        /// <summary>
        ///     工厂Werks
        /// </summary>
        public string Werks { get; set; }

        /// <summary>
        /// 采购凭证
        /// </summary>
        public string Ebeln_input { get; set; }

        /// <summary>
        /// 采购组织
        /// </summary>
        public string Ekorg { get; set; }

        /// <summary>
        /// 过账日期
        /// </summary>
        public string Budat { get; set; }

        /// <summary>
        /// 库存地点
        /// </summary>
        public string Lgort { get; set; }

        /// <summary>
        /// 采购凭证号
        /// </summary>
        public string Ebeln { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string Ebelp { get; set; }

        /// <summary>
        /// 版面
        /// </summary>
        public string Zbanm { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string Zpdte { get; set; }

        /// <summary>
        /// 素铁物料编号
        /// </summary>
        public string Zmatn { get; set; }

        /// <summary>
        /// 铁镀锡量
        /// </summary>
        public string Ztdxl { get; set; }

        /// <summary>
        /// 铁硬度
        /// </summary>
        public string Ztydz { get; set; }

        /// <summary>
        /// 来铁厂家
        /// </summary>
        public string Zltcj { get; set; }

        /// <summary>
        /// 素铁重量
        /// </summary>
        public decimal Szmeng { get; set; }

        /// <summary>
        /// 素铁单位
        /// </summary>
        public string Szment { get; set; }

        /// <summary>
        /// 素铁资源号
        /// </summary>
        public string Ztres { get; set; }

        /// <summary>
        /// 打印操作ID
        /// </summary>
        public string Zgrop { get; set; }

    }
}