﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;

using Nest.Resolvers;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}
	/// </pre>
	/// {indices} is optional but AllIndices() needs to be explicitly called.
	/// </summary>
	public class IndicesOptionalExplicitAllPathDescriptor<P, K> : BasePathDescriptor<P>
		where P : IndicesOptionalExplicitAllPathDescriptor<P, K>, new()
		where K : FluentRequestParameters<K>, new()
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		
		internal bool? _AllIndices { get; set; }

		public P AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (P)this;
		}
			
		public P Index(string index)
		{
			return this.Indices(index);
		}
	
		public P Index<T>() where T : class
		{
			return this.Indices(typeof(T));
		}
			
		public P Indices(params string[] indices)
		{
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return (P)this;
		}

		public P Indices(params Type[] indicesTypes)
		{
			this._Indices = indicesTypes.Select(s=>(IndexNameMarker)s);
			return (P)this;
		}

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentRequestParameters<K>, new()
		{
			var inferrer = new ElasticInferrer(settings);
			if (!this._AllIndices.HasValue && this._Indices == null)
				this._Indices = new[] {(IndexNameMarker)inferrer.DefaultIndex};

			string index = "_all";
			if (!this._AllIndices.GetValueOrDefault(false))
				index = string.Join(",", this._Indices.Select(inferrer.IndexName));

			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = index,
			};
			pathInfo.RequestParameters = queryString ?? new K();
			pathInfo.RequestParameters.RequestConfiguration(r=>this._RequestConfiguration);
			return pathInfo;
		}

	}
}
