using UnityEngine;
using UnityEngine.EventSystems;

public class MonoBehaviourBase : MonoBehaviour
{
    protected virtual void Awake() { }
    protected virtual void Start() { }

    protected virtual void Update() { }

    protected virtual void LateUpdate() { }

    protected virtual void FixedUpdate() { }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected virtual void OnDestroy() { }

    protected virtual void OnGUI() { }

    protected virtual void OnApplicationFocus(bool hasFocus) { }

    protected virtual void OnApplicationPause(bool pauseStatus) { }

    protected virtual void OnMouseEnter() { }//鼠标进入区域
    protected virtual void OnMouseOver() { }//鼠标停留在区域内
    protected virtual void OnMouseExit() { }//鼠标离开区域
    protected virtual void OnMouseDown() { }//鼠标按下
    protected virtual void OnMouseUp() { }  //鼠标松开
    protected virtual void OnMouseDrag() { }//鼠标拖拽
    protected virtual void OnMouseUpAsButton() { }

    protected virtual void OnTriggerEnter(Collider target) { }
    protected virtual void OnTriggerExit(Collider target) { }
    protected virtual void OnTriggerStay(Collider target) { }

    protected virtual void OnCollisionEnter(Collision target) { }
    protected virtual void OnCollisionExit(Collision target) { }
    protected virtual void OnCollisionStay(Collision target) { }

    protected virtual void OnDrawGizmos() { }
    protected virtual void OnDrawGizmosSelected() { }
    protected virtual void OnValidate() { }

    public virtual void OnEndDrag(PointerEventData eventData) { }
    public virtual void OnDrag(PointerEventData eventData) { }
    public virtual void OnBeginDrag(PointerEventData eventData) { }

    public virtual void OnPointerDown(PointerEventData eventData) { }
    public virtual void OnPointerUp(PointerEventData eventData) { }
}