using UnityEngine;

public class BasicEntity : MonoBehaviour
{
    public string ID_ENTITY {get; set;}

    public virtual void SetAnimationWalking(string state, Animator animator)
    {
        int IDHash = Animator.StringToHash(state);
        
        Transform GetEntity = gameObject.transform;

        if(GetEntity.position != Vector3.zero)
        {
            animator.SetBool(IDHash, true);
        }
        else
        {
            animator.SetBool(IDHash, false);
        }
    }

    public virtual void SetAnimationIdle(string state, Animator animator)
    {
        //implemment idle animation soon
    }
}
