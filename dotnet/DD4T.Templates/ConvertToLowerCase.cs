﻿using System;
using System.Xml;
using System.Collections.Generic;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;
using Dynamic = DD4T.ContentModel;
using DD4T.Templates.Base.Utils;
using DD4T.Templates.Base;

namespace DD4T.Templates {

	[TcmTemplateTitle("Convert to lower case")]
	public class ConvertToLowerCase : ITemplate {

		private TemplatingLogger log = TemplatingLogger.GetLogger(typeof(ConvertToLowerCase));
		private Package package;
		private Engine engine;
		private List<XmlNode> toBeRemoved = new List<XmlNode>();

		public void Transform(Engine engine, Package package) {
			this.package = package;
			this.engine = engine;

         if (engine.PublishingContext.RenderContext != null && engine.PublishingContext.RenderContext.ContextVariables.Contains(BasePageTemplate.VariableNameCalledFromDynamicDelivery))
         {
            if (engine.PublishingContext.RenderContext.ContextVariables[BasePageTemplate.VariableNameCalledFromDynamicDelivery].Equals(BasePageTemplate.VariableValueCalledFromDynamicDelivery))
            {
               log.Debug("template is rendered by a DynamicDelivery page template, will not convert to lower case");
               return;
            }
         }

			Item outputItem = package.GetByName("Output");
			String inputValue = package.GetValue("Output");
			if (inputValue == null || inputValue.Length == 0) {
				log.Error("Could not find 'Output' in the package. Exiting template.");
				return;
			}

            string convertedValue = LowerCaseConverter.Convert(inputValue);

			package.Remove(outputItem);
			outputItem.SetAsString(convertedValue);
			package.PushItem("Output", outputItem);
		}



		private void ConvertNodeToLowerCase(XmlNode n) {
			if (!(n is XmlElement)) {
				return;
			}
			string firstLetter = n.LocalName.Substring(0,1);
			log.Debug("elmt " + n.LocalName);
			if (!firstLetter.Equals(firstLetter.ToLower())) {

				XmlElement newElmt = n.OwnerDocument.CreateElement(firstLetter.ToLower() + n.LocalName.Substring(1));
				foreach (XmlNode c in n.ChildNodes) {
					newElmt.AppendChild(c.CloneNode(true));
				}
				XmlNodeList children = newElmt.ChildNodes;
				foreach (XmlNode c in children) {
					ConvertNodeToLowerCase(c);
				}
//				toBeRemoved.Add(n);
				n.ParentNode.RemoveChild(n);
				log.Debug("element is now " + newElmt.OuterXml);
			} else {
				XmlNodeList children = n.ChildNodes;
				foreach (XmlNode c in children) {
					ConvertNodeToLowerCase(c);
				}
			}

		}

	}

}