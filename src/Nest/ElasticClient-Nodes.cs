﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public INodeInfoResponse NodesInfo(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<NodesInfoDescriptor, NodesInfoRequestParameters, NodeInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.NodesInfoDispatch<NodeInfoResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<INodeInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<NodesInfoDescriptor, NodesInfoRequestParameters, NodeInfoResponse, INodeInfoResponse>(
				selector,
				(p, d) => this.RawDispatch.NodesInfoDispatchAsync<NodeInfoResponse>(p)
			);
		}

		/// <inheritdoc />
		public INodeStatsResponse NodesStats(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<NodesStatsDescriptor, NodesStatsRequestParameters, NodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.NodesStatsDispatch<NodeStatsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<INodeStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<NodesStatsDescriptor, NodesStatsRequestParameters, NodeStatsResponse, INodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.NodesStatsDispatchAsync<NodeStatsResponse>(p)
			);
		}
	}
}