using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wing_ding_pong.CollidableObjects;
using wing_ding_pong._2D;
using wing_ding_pong.Traits;

namespace wing_ding_pong
{
	public class Rules
	{
        private IDictionary<string, object> _collisionRulesRegister;
        
        public void RegisterRule<T1, T2>(ObjectCollisionRulesTraits trait)
        {
          if (_collisionRulesRegister == null)
          {
            _collisionRulesRegister = new Dictionary<string, object>();
          }
          _collisionRulesRegister[typeof(T1).Name + typeof(T2).Name] = trait;
        }

        public ObjectCollisionRulesTraits GetRule(string className1, string className2)
        {
          if (_collisionRulesRegister.ContainsKey(className1 + className2))
          {
            return _collisionRulesRegister[className1 + className2] as ObjectCollisionRulesTraits;
          }
          else
          {
            return null;
          }
        }

        public void ProcessCollsions(Collidable2DBase obj1, Collidable2DBase obj2, Vector obj1PosDp, 
                                        Vector obj1CollDirection, Vector obj2PosDp, Vector obj2CollDirection)
        {
            ObjectCollisionRulesTraits ruleTrait;

            ruleTrait = this.GetRule(obj1.ObjectName, obj2.ObjectName);
            if (ruleTrait != null)
            {
                ruleTrait.ResolveStaticObjectStaticObjectCollision(obj1, obj2, obj1PosDp, obj1CollDirection,
                                                                    obj2PosDp, obj2CollDirection);
            }
            ruleTrait = this.GetRule(obj2.ObjectName, obj1.ObjectName);
            if (ruleTrait != null)
            {
                ruleTrait.ResolveStaticObjectStaticObjectCollision(obj2, obj1, obj2PosDp, obj2CollDirection,
                                                                    obj1PosDp, obj1CollDirection);
            }
        }
	}
}
