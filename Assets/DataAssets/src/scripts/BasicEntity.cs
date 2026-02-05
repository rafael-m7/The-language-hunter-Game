using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Basic class for all living-entitys
/// </summary>
public class BasicEntity : MonoBehaviour
{
    public string ID_ENTITY {get; set;}

    private int Health {get; set;}

    protected Dictionary<string, int> HashAnimations = new Dictionary<string, int>();

    protected virtual void Awake()
    {

        RegisterAnimation("OnMoving");
        RegisterAnimation("OnIdle");

        Health = 100;
    }

    protected void RegisterAnimation(string name)
    {
        if (!HashAnimations.ContainsKey(name))
        {
            HashAnimations.Add(name, Animator.StringToHash(name));
        }
    }

    protected virtual void SetAnimationWalking(Animator animator, bool IsMoving)
    {
        if(HashAnimations.TryGetValue("OnMoving", out int hash))
        {
            animator.SetBool(hash, IsMoving);
        }
    }

    protected virtual void SetAnimationIdle(string state, Animator animator)
    {
        //implemment idle animation soon
    }
}
