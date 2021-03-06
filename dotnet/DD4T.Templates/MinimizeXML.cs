﻿using System;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;
using DD4T.Templates.Base;
using DD4T.Templates.Base.Utils;

namespace DD4T.Templates.XML
{

    [TcmTemplateTitle("Minimize XML")]
    public class MinimizeXML : ITemplate
    {
        protected TemplatingLogger log = TemplatingLogger.GetLogger(typeof(ConvertXmlToJava));
        protected Package package;
        protected Engine engine;


        public void Transform(Engine engine, Package package)
        {
            this.package = package;
            this.engine = engine;

            if (engine.PublishingContext.RenderContext != null && engine.PublishingContext.RenderContext.ContextVariables.Contains(BasePageTemplate.VariableNameCalledFromDynamicDelivery))
            {
                if (engine.PublishingContext.RenderContext.ContextVariables[BasePageTemplate.VariableNameCalledFromDynamicDelivery].Equals(BasePageTemplate.VariableValueCalledFromDynamicDelivery))
                {
                    log.Debug("template is rendered by a DynamicDelivery page template, will not convert from XML to java");
                    return;
                }
            }

            Item outputItem = package.GetByName("Output");
            String inputValue = package.GetValue("Output");

            if (inputValue == null || inputValue.Length == 0)
            {
                log.Warning("Could not find 'Output' in the package, nothing to transform");
                return;
            }

            String minimizeSettings = package.GetValue("MinimizeSettings");


            string outputValue = XmlMinimizer.Convert(inputValue, minimizeSettings);

            // replace the Output item in the package
            package.Remove(outputItem);
            outputItem.SetAsString(outputValue);
            package.PushItem("Output", outputItem);
        }

    }
}
