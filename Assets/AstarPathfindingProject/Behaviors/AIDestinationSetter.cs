using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

namespace Pathfinding
{
    /// <summary>
    /// Sets the destination of an AI to the position of a specified object.
    /// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
    /// This component will then make the AI move towards the <see cref="target"/> set on this component.
    ///
    /// See: <see cref="Pathfinding.IAstarAI.destination"/>
    ///
    /// [Open online documentation to see images]
    /// </summary>
    [UniqueComponent(tag = "ai.destination")]
    [HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
    public class AIDestinationSetter : VersionedMonoBehaviour
    {
        /// <summary>The object that the AI should move to</summary>
        public Transform target;

        public LayerMask layerMask;

        IAstarAI ai;

        void OnEnable()
        {
            ai = GetComponent<IAstarAI>();
            // Update the destination right before searching for a path as well.
            // This is enough in theory, but this script will also update the destination every
            // frame as the destination is used for debugging and may be used for other things by other
            // scripts as well. So it makes sense that it is up to date every frame.
            if (ai != null) ai.onSearchPath += Update;
        }

        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }

        /// <summary>Updates the AI's destination every frame</summary>
        void Update()
        {
            if (target != null && ai != null) ai.destination = target.position;
            if (!GetComponent<AIDestinationSetter>().enabled) return;
            if (!Input.GetMouseButtonDown(1)) return;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit; // RaycastHit2D kullanılacak

            if (!Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask)) return;
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity,layerMask);
            target = hit.transform;
            GetComponent<SpriteRenderer>().color = Color.black;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!GetComponent<AIDestinationSetter>().enabled&& other.gameObject!= null) return;
            if (other.gameObject == target.gameObject )
            {
                target = null;
                GetComponent<AIDestinationSetter>().enabled = false;
            }
        }
    }
}