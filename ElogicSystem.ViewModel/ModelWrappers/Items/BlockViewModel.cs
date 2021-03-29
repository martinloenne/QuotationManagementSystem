using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElogicSystem.Model;

namespace ElogicSystem.ViewModel {

  public class BlockViewModel : ContainerViewModel {
    public Block BlockModel { get; }

    public BlockViewModel(Block block) : base(block) {
      BlockModel = block;
    }

    public BlockViewModel(RelationItem relationItem) : base(relationItem) {
    }
  }
}