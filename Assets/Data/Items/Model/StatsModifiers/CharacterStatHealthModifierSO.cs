using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float value)
    {
        Player_status playerStatus = character.GetComponent<Player_status>();
        if (playerStatus != null)
        {
            playerStatus.Heal((int)value);
        }
    }

}
