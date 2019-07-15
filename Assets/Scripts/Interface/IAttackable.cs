using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    IEnumerator Attack(Human human);
}
