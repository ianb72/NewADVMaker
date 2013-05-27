using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewADVMaker
{
    public class Characteristics
    {
        public AssType GetAssType(BodyType bodytype)
        {
            Dictionary<BodyType, AssType> assLUT = new Dictionary<BodyType, AssType>();
            assLUT.Add(BodyType.athletic, AssType.muscled);
            assLUT.Add(BodyType.average,AssType.round);
            assLUT.Add(BodyType.chubby, AssType.chubby);
            assLUT.Add(BodyType.fat, AssType.fat);
            assLUT.Add(BodyType.fit, AssType.tight);
            assLUT.Add(BodyType.petite, AssType.tight);
            assLUT.Add(BodyType.young, AssType.tight);

            return assLUT[bodytype];
        }

    }
    public enum CupSize
    {
        A,
        AA,
        B,
        C,
        D,
        DD,
        E,
        EE,
        F,
        FF,
        G,
        H
    }
    public enum FirstName
    {
        Mari,
        Jane,
        Wendy,
        Judy,
        Bella,
        Lauren,
        Abbey,
        Emma,
        Linda,
        Jen,
        Lucy,
        Marilyn,
        Bernice,
        Dawn,
        Diedre,
        Delphine,
        Kelly,
        Mandy,
        Stacey,
        Claire,
        Shirley,
        Kerry,
        Amanda,
        Holly,
        Fern,
        Catriona,
        Beth,
        John,
        James,
        Jim,
        Charlie,
        Steven,
        Mike,
        Barry,
        Matt,
        Darren,
        Phil,
        Lucien,
        Ian,
        Kevin,
        Paul,
        Peter,
        Michael,
        Chad,
        Tim,
        Timothy,
        Stephen,
        Adam,
        Mark,
        Francis,
        Lindsay
    }
    public enum FirstNameFemale
    {
        Mari,
        Jane,
        Wendy,
        Judy,
        Bella,
        Lauren,
        Abbey,
        Emma,
        Linda,
        Jen,
        Lucy,
        Marilyn,
        Bernice,
        Dawn,
        Diedre,
        Delphine,
        Kelly,
        Mandy,
        Stacey,
        Claire,
        Shirley,
        Kerry,
        Amanda,
        Holly,
        Fern,
        Catriona,
        Beth,
        Francis,
        Lindsay
    }
    public enum FirstNameMale
    {
        John,
        James,
        Jim,
        Charlie,
        Steven,
        Mike,
        Barry,
        Matt,
        Darren,
        Phil,
        Lucien,
        Ian,
        Kevin,
        Paul,
        Peter,
        Michael,
        Chad,
        Tim,
        Timothy,
        Stephen,
        Adam,
        Mark
    }
    public enum Surname
    {
        Butterworth,
        Smith,
        Williams,
        Stewart,
        Spiers,
        Williamson,
        Ball,
        Poole,
        Shearer,
        Duffy
    }
    public enum Gender
    {
        None,
        Male,
        Female,
        Shemale,
        Herm
    }
    public enum RelationshipFemale
    {
        stranger,
        neighbor,
        daughter,
        niece,
        sister,
        friend,
        bestfriend,
        girlfriend,
        fiance,
        wife,
        aunt,
        mum,
        granny,
        cousin
    }
    public enum RelationshipMale
    {
        stranger,
        neighbor,
        son,
        nephew,
        brother,
        friend,
        bestfriend,
        boyfriend,
        fiance,
        husband,
        uncle,
        dad,
        grandad,
        cousin
    }
    public enum RelationShipAll
    {
        stranger,
        neighbor,
        daughter,
        niece,
        sister,
        friend,
        girlfriend,
        wife,
        aunt,
        mum,
        granny,
        son,
        nephew,
        brother,
        boyfriend,
        fiance,
        husband,
        uncle,
        dad,
        grandad,
        bestfriend,
        cousin
    }
    public enum BloodRelation
    {
        daughter,
        sister,
        mum,
        granny,
        son,
        brother,
        dad,
        grandad,
        cousin,
        aunt,
        uncle
    }
    public enum SameSurname
    {
        daughter,
        sister,
        wife,
        mum,
        granny,
        son,
        brother,
        husband,
        dad,
        grandad,
    }
    public enum NonVirgin
    {
        mum,
        dad,
        granny,
        grandad,
        wife,
        husband,
        fiance,
        boyfriend,
        girlfriend
    }
    public enum Sexuality
    {
        normal,
        slut,
        frigid,
        easy,
        loving,
        virgin
    }
    public enum Personality
    {
        nice,
        nasty,
        dominant,
        submissive,
        normal,
        loving
    }
    public enum PussyType
    {
        loose,
        tight,
        virgin,
        old,
        wrinkly,
        lippy,
        wellFucked
    }
    public enum CockType
    {
        cut,
        uncircumsised,
        veiny,
        smooth,
        young,
    }
    public enum TitType
    {
        big,
        droopy,
        perfect,
        round,
        silicone,
        flat,
        young,
        small
    }
    public enum AssholeType
    {
        tight,
        virgin,
        loose,
        wellFucked
    }
    public enum SexualPreference
    {
        bi,
        straight,
        lesbian,
        gay
    }
    public enum BodyType
    {
        fit,
        athletic,
        average,
        fat,
        chubby,
        petite,
        young
    }
    public enum Feelings
    {
        
    }
    public enum AssType
    {
        tight,
        chubby,
        fat,
        round,
        muscled
    }
    public enum PussyState
    {
        lovely,
        moist,
        damp,
        wet,
        verywet,
        dripping
    }
    public enum BodyPart
    {
        face,
        tits,
        ass,
        cunt,
        legs,
        feet
    }
    public enum PubicHair
    {
        shaved,
        smooth,
        shaped,
        hairy,
        hairless,
        downy
    }
    public enum RoomType
    {
        bedroom,
        living,
        stairs,
        hall,
        kitchen
    }
    public enum Exits
    {
        north,
        south,
        east,
        west,
    }
}
