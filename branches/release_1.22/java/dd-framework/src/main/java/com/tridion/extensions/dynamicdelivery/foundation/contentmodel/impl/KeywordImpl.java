/**  
 *  Copyright 2011 Capgemini & SDL
 * 
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 * 
 *        http://www.apache.org/licenses/LICENSE-2.0
 * 
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 */
package com.tridion.extensions.dynamicdelivery.foundation.contentmodel.impl;

import org.simpleframework.xml.Attribute;

import com.tridion.extensions.dynamicdelivery.foundation.contentmodel.Keyword;

public class KeywordImpl extends BaseItem implements Keyword {


	@Attribute(name = "taxonomyId")
	private String taxonomyId;
	
	@Attribute(name = "path")
	private String path;


	@Override
	public String getTaxonomyId() {
		return taxonomyId;
	}

	@Override
	public void setTaxonomyId(String taxonomyId) {
		this.taxonomyId = taxonomyId;
	}

	
	@Override
	public String getPath() {
		return path;
	}

	@Override
	public void setPath(String path) {
		this.path = path;
	}


}
