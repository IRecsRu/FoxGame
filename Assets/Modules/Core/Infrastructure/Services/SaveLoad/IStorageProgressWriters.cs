using System.Collections.Generic;
using Modules.Core.Infrastructure.Services.PersistentProgress;

namespace Modules.Core.Infrastructure.Services.SaveLoad
{
	public interface IStorageProgressWriters
	{
		public List<ISavedProgress> ProgressWriters { get; }
	}
}