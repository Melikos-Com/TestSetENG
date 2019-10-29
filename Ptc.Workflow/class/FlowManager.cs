using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Workflow
{
    public static class FlowManager
    {
        /// <summary>
        /// 流程清單
        /// </summary>
        public static IDictionary<FlowType, IFlow> _flows { get; private set; } = new Dictionary<FlowType, IFlow>();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="type"></param>
        /// <param name="flow"></param>
        public static void Add(FlowType type, IFlow flow)
        {

            if (_flows.ContainsKey(type))
                throw new IndexOutOfRangeException($"already added this type:{type}");

            _flows.Add(type, flow);
        }
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="type"></param>
        public static void Remove(FlowType type)
        {

            if (!_flows.ContainsKey(type))
                throw new IndexOutOfRangeException($"not exist type:{type}");

            _flows.Remove(type);

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="type"></param>
        /// <param name="flow"></param>
        public static void Update(FlowType type, IFlow flow)
        {

            if (_flows.ContainsKey(type))
                _flows[type] = flow;

        }
        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="type"></param>
        public static void Clear(FlowType type)
        {
            _flows.Clear();
        }
        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static Boolean Next(this ILog log)
        {
            //找到log的 nextSts
            int nextSts = log.NextSts;

            //找到log的 flowType
            FlowType type = log.FlowType;

            return _flows[type]._steps[nextSts]
                               .Excute(log);

        }
        
       

    }
}
