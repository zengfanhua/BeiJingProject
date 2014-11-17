using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Print
{
    public class PrintInfo
    {
        //产品名称
        public string ProName { get; set; }
        //产品编码
        public string ProNum { get; set; }
        //批次
        public string Batch { get; set; }
        //包/桶号
        public string PackNum { get; set; }
        //数量
        public string Quantity { get; set; }
        //生产日期
        public string ProDate { get; set; }
        //供应商批次
        public string SupplyBatch { get; set; }
        //工厂
        public string Factory { get; set; }
        //铁镀锡量/铁硬度
        public string HardNess { get; set; }
        //素铁编号/厂家
        public string IronNum { get; set; }
        //素铁重量
        public string IronWeight { get; set; }
        //素铁资源号
        public string ResourceNum { get; set; }
        //采购订单号
        public string OrderNum { get; set; }
        //采购订单行项目
        public string OrderItem { get; set; }
        //版面
        public string Layout { get; set; }
        //计量单位
        public string Unit { get; set; }
        //素铁批次
        public string IronBatch { get; set; }
        //素铁重量单位
        public string IronUnit { get; set; }
    }
}
