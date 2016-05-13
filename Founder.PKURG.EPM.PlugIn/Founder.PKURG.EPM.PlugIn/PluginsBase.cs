using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;
namespace Founder.PKURG.EPM.PlugIn
{
    public class PluginsBase
    {
        private IPluginExecutionContext pluginExecutionContext;
        private IOrganizationService organizationService;
        private ITracingService tracingService;
        /// <summary>
        /// 初始化组织服务和插件上下文对象
        /// </summary>
        /// <param name="serviceProvider">服务提供者对象</param>
        protected void Initialize(IServiceProvider serviceProvider)
        {
            //插件上下文环境
            this.pluginExecutionContext = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            //组织服务工厂
            IOrganizationServiceFactory organizationServiceFactory = (IOrganizationServiceFactory)
                serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            //组织服务
            this.organizationService = organizationServiceFactory.CreateOrganizationService(this.pluginExecutionContext.InitiatingUserId);//this.pluginExecutionContext.UserId

            this.tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
        }

        /// <summary>
        /// 获取组织服务
        /// </summary>
        public IOrganizationService OrgService
        {
            get
            {
                return this.organizationService;
            }
        }

        /// <summary>
        /// 获取插件上下文对象
        /// </summary>
        public IPluginExecutionContext Context
        {
            get
            {
                return this.pluginExecutionContext;
            }
        }
        public ITracingService Tracing
        {
            get
            {
                return this.tracingService;
            }
        }
    }
}
