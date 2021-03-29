using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElogicSystem.Model {

  public static class RelationItemFactory {

    public static RelationItem GetRelationItem(Item item, IRelationItemsOwner owner) {
      switch (owner) {
        case Container c:
          return new ContainerItem(owner as Container, item);

        case Template t:
          return new TemplateItem(owner as Template, item);

        case Panel p:
          return new PanelItem(owner as Panel, item);

        default:
          throw new ArgumentException("Could determine type of owner");
      }
    }
  }
}