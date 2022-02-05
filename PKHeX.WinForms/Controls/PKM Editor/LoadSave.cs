﻿using System;
using PKHeX.Core;
using PKHeX.Drawing;
using PKHeX.Drawing.PokeSprite;

namespace PKHeX.WinForms.Controls
{
    public partial class PKMEditor
    {
        private void LoadNickname(PKM pk)
        {
            CHK_Nicknamed.Checked = pk.IsNicknamed;
            TB_Nickname.Text = pk.Nickname;
        }

        private void SaveNickname(PKM pk)
        {
            pk.IsNicknamed = CHK_Nicknamed.Checked;
            pk.Nickname = TB_Nickname.Text;
        }

        private void LoadSpeciesLevelEXP(PKM pk)
        {
            if (!HaX)
            {
                // Sanity check level and EXP
                var current = pk.CurrentLevel;
                if (current == 100) // clamp back to max EXP
                    pk.CurrentLevel = 100;
            }

            CB_Species.SelectedValue = pk.Species;
            TB_Level.Text = pk.Stat_Level.ToString();
            TB_EXP.Text = pk.EXP.ToString();
        }

        private void SaveSpeciesLevelEXP(PKM pk)
        {
            pk.Species = WinFormsUtil.GetIndex(CB_Species);
            pk.EXP = Util.ToUInt32(TB_EXP.Text);
            pk.Stat_Level = Util.ToInt32(TB_Level.Text);
        }

        private void LoadOT(PKM pk)
        {
            GB_OT.BackgroundImage = null; // clear the Current Handler indicator just in case we switched formats.
            TB_OT.Text = pk.OT_Name;
            Label_OTGender.Text = gendersymbols[pk.OT_Gender];
            Label_OTGender.ForeColor = Draw.GetGenderColor(pk.OT_Gender);
        }

        private void SaveOT(PKM pk)
        {
            pk.OT_Name = TB_OT.Text;
            pk.OT_Gender = PKX.GetGenderFromString(Label_OTGender.Text);
        }

        private void LoadPKRS(PKM pk)
        {
            Label_PKRS.Visible = CB_PKRSStrain.Visible = CHK_Infected.Checked = Label_PKRSdays.Visible = CB_PKRSDays.Visible = pk.PKRS_Infected;
            CB_PKRSStrain.SelectedIndex = pk.PKRS_Strain;
            CHK_Cured.Checked = pk.PKRS_Cured;
            CB_PKRSDays.SelectedIndex = Math.Min(CB_PKRSDays.Items.Count - 1, pk.PKRS_Days); // clamp to valid day values for the current strain
        }

        private void SavePKRS(PKM pk)
        {
            pk.PKRS_Days = CB_PKRSDays.SelectedIndex;
            pk.PKRS_Strain = CB_PKRSStrain.SelectedIndex;
        }

        private void LoadIVs(PKM pk) => Stats.LoadIVs(pk.IVs);
        private void LoadEVs(PKM pk) => Stats.LoadEVs(pk.EVs);
        private void LoadAVs(IAwakened pk) => Stats.LoadAVs(pk);
        private void LoadGVs(IGanbaru pk) => Stats.LoadGVs(pk);

        private void LoadMoves(PKM pk)
        {
            CB_Move1.SelectedValue = pk.Move1;
            CB_Move2.SelectedValue = pk.Move2;
            CB_Move3.SelectedValue = pk.Move3;
            CB_Move4.SelectedValue = pk.Move4;
            CB_PPu1.SelectedIndex = pk.Move1_PPUps;
            CB_PPu2.SelectedIndex = pk.Move2_PPUps;
            CB_PPu3.SelectedIndex = pk.Move3_PPUps;
            CB_PPu4.SelectedIndex = pk.Move4_PPUps;
            TB_PP1.Text = pk.Move1_PP.ToString();
            TB_PP2.Text = pk.Move2_PP.ToString();
            TB_PP3.Text = pk.Move3_PP.ToString();
            TB_PP4.Text = pk.Move4_PP.ToString();
        }

        private void SaveMoves(PKM pk)
        {
            pk.Move1 = WinFormsUtil.GetIndex(CB_Move1);
            pk.Move2 = WinFormsUtil.GetIndex(CB_Move2);
            pk.Move3 = WinFormsUtil.GetIndex(CB_Move3);
            pk.Move4 = WinFormsUtil.GetIndex(CB_Move4);
            pk.Move1_PP = WinFormsUtil.GetIndex(CB_Move1) > 0 ? Util.ToInt32(TB_PP1.Text) : 0;
            pk.Move2_PP = WinFormsUtil.GetIndex(CB_Move2) > 0 ? Util.ToInt32(TB_PP2.Text) : 0;
            pk.Move3_PP = WinFormsUtil.GetIndex(CB_Move3) > 0 ? Util.ToInt32(TB_PP3.Text) : 0;
            pk.Move4_PP = WinFormsUtil.GetIndex(CB_Move4) > 0 ? Util.ToInt32(TB_PP4.Text) : 0;
            pk.Move1_PPUps = WinFormsUtil.GetIndex(CB_Move1) > 0 ? CB_PPu1.SelectedIndex : 0;
            pk.Move2_PPUps = WinFormsUtil.GetIndex(CB_Move2) > 0 ? CB_PPu2.SelectedIndex : 0;
            pk.Move3_PPUps = WinFormsUtil.GetIndex(CB_Move3) > 0 ? CB_PPu3.SelectedIndex : 0;
            pk.Move4_PPUps = WinFormsUtil.GetIndex(CB_Move4) > 0 ? CB_PPu4.SelectedIndex : 0;
        }

        private void LoadShadow3(IShadowPKM pk)
        {
            NUD_ShadowID.Value = pk.ShadowID;
            FLP_Purification.Visible = pk.ShadowID > 0;
            if (pk.ShadowID > 0)
            {
                int value = pk.Purification;
                if (value < NUD_Purification.Minimum)
                    value = (int)NUD_Purification.Minimum;

                NUD_Purification.Value = value;
                CHK_Shadow.Checked = pk.IsShadow;

                NUD_ShadowID.Value = Math.Max(pk.ShadowID, 0);
            }
            else
            {
                NUD_Purification.Value = 0;
                CHK_Shadow.Checked = false;
                NUD_ShadowID.Value = 0;
            }
        }

        private void SaveShadow3(IShadowPKM pk)
        {
            pk.ShadowID = (int)NUD_ShadowID.Value;
            if (pk.ShadowID > 0)
                pk.Purification = (int)NUD_Purification.Value;
        }

        private void LoadRelearnMoves(PKM pk)
        {
            CB_RelearnMove1.SelectedValue = pk.RelearnMove1;
            CB_RelearnMove2.SelectedValue = pk.RelearnMove2;
            CB_RelearnMove3.SelectedValue = pk.RelearnMove3;
            CB_RelearnMove4.SelectedValue = pk.RelearnMove4;
        }

        private void SaveRelearnMoves(PKM pk)
        {
            pk.RelearnMove1 = WinFormsUtil.GetIndex(CB_RelearnMove1);
            pk.RelearnMove2 = WinFormsUtil.GetIndex(CB_RelearnMove2);
            pk.RelearnMove3 = WinFormsUtil.GetIndex(CB_RelearnMove3);
            pk.RelearnMove4 = WinFormsUtil.GetIndex(CB_RelearnMove4);
        }

        private void LoadMisc1(PKM pk)
        {
            LoadSpeciesLevelEXP(pk);
            LoadNickname(pk);
            LoadOT(pk);
            LoadIVs(pk);
            LoadEVs(pk);
            LoadMoves(pk);
        }

        private void SaveMisc1(PKM pk)
        {
            SaveSpeciesLevelEXP(pk);
            SaveNickname(pk);
            SaveOT(pk);
            SaveMoves(pk);
        }

        private void LoadMisc2(PKM pk)
        {
            LoadPKRS(pk);
            CHK_IsEgg.Checked = pk.IsEgg;
            CB_HeldItem.SelectedValue = pk.HeldItem;
            CB_Form.SelectedIndex = CB_Form.Items.Count > pk.Form ? pk.Form : CB_Form.Items.Count - 1;
            if (pk is IFormArgument f)
                FA_Form.LoadArgument(f, pk.Species, pk.Form, pk.Format);

            ReloadToFriendshipTextBox(pk);

            Label_HatchCounter.Visible = CHK_IsEgg.Checked;
            Label_Friendship.Visible = !CHK_IsEgg.Checked;
        }

        private void ReloadToFriendshipTextBox(PKM pk)
        {
            // Show OT friendship always if it is an egg.
            var fs = (pk.IsEgg ? pk.OT_Friendship : pk.CurrentFriendship);
            TB_Friendship.Text = fs.ToString();
        }

        private void SaveMisc2(PKM pk)
        {
            SavePKRS(pk);
            pk.IsEgg = CHK_IsEgg.Checked;
            pk.HeldItem = WinFormsUtil.GetIndex(CB_HeldItem);
            pk.Form = CB_Form.Enabled ? CB_Form.SelectedIndex & 0x1F : 0;
            if (Entity is IFormArgument f)
                FA_Form.SaveArgument(f);

            var friendship = Util.ToInt32(TB_Friendship.Text);
            UpdateFromFriendshipTextBox(pk, friendship);
        }

        private static void UpdateFromFriendshipTextBox(PKM pk, int friendship)
        {
            if (pk.IsEgg)
                pk.OT_Friendship = friendship;
            else
                pk.CurrentFriendship = friendship;
        }

        private void LoadMisc3(PKM pk)
        {
            TB_PID.Text = $"{pk.PID:X8}";
            Label_Gender.Text = gendersymbols[Math.Min(2, pk.Gender)];
            Label_Gender.ForeColor = Draw.GetGenderColor(pk.Gender);
            CB_Nature.SelectedValue = pk.Nature;
            CB_Language.SelectedValue = pk.Language;
            CB_GameOrigin.SelectedValue = pk.Version;
            CB_Ball.SelectedValue = pk.Ball;
            CB_MetLocation.SelectedValue = pk.Met_Location;
            TB_MetLevel.Text = pk.Met_Level.ToString();
            CHK_Fateful.Checked = pk.FatefulEncounter;

            if (pk is IContestStats s)
                s.CopyContestStatsTo(Contest);

            TID_Trainer.LoadIDValues(pk);

            // Load Extrabyte Value
            var offset = Convert.ToInt32(CB_ExtraBytes.Text, 16);
            var value = pk.Data[offset];
            TB_ExtraByte.Text = value.ToString();
        }

        private void SaveMisc3(PKM pk)
        {
            pk.PID = Util.GetHexValue(TB_PID.Text);
            pk.Nature = WinFormsUtil.GetIndex(CB_Nature);
            pk.Gender = PKX.GetGenderFromString(Label_Gender.Text);

            if (pk is IContestStatsMutable s)
                Contest.CopyContestStatsTo(s);

            pk.FatefulEncounter = CHK_Fateful.Checked;
            pk.Ball = WinFormsUtil.GetIndex(CB_Ball);
            pk.Version = WinFormsUtil.GetIndex(CB_GameOrigin);
            pk.Language = WinFormsUtil.GetIndex(CB_Language);
            pk.Met_Level = Util.ToInt32(TB_MetLevel.Text);
            pk.Met_Location = WinFormsUtil.GetIndex(CB_MetLocation);
        }

        private void LoadMisc4(PKM pk)
        {
            CAL_MetDate.Value = pk.MetDate ?? new DateTime(2000, 1, 1);
            if (Locations.IsNoneLocation((GameVersion)pk.Version, pk.Egg_Location))
            {
                CHK_AsEgg.Checked = GB_EggConditions.Enabled = false;
                CAL_EggDate.Value = new DateTime(2000, 01, 01);
            }
            else
            {
                // Was obtained initially as an egg.
                CHK_AsEgg.Checked = GB_EggConditions.Enabled = true;
                CAL_EggDate.Value = pk.EggMetDate ?? new DateTime(2000, 1, 1);
            }
            CB_EggLocation.SelectedValue = pk.Egg_Location;
        }

        private void SaveMisc4(PKM pk)
        {
            pk.MetDate = CAL_MetDate.Value;

            // Default Dates
            DateTime? egg_date = null;
            int egg_location = Locations.GetNoneLocation((GameVersion)pk.Version);
            if (CHK_AsEgg.Checked) // If encountered as an egg, load the Egg Met data from fields.
            {
                egg_date = CAL_EggDate.Value;
                egg_location = WinFormsUtil.GetIndex(CB_EggLocation);
            }
            // Egg Met Data
            pk.EggMetDate = egg_date;
            pk.Egg_Location = egg_location;
            if (pk.IsEgg && Locations.IsNoneLocation((GameVersion)pk.Version, pk.Met_Location)) // If still an egg, it has no hatch location/date. Zero it!
                pk.MetDate = null;

            pk.Ability = WinFormsUtil.GetIndex(HaX ? DEV_Ability : CB_Ability);
        }

        private void LoadMisc6(PKM pk)
        {
            TB_EC.Text = $"{pk.EncryptionConstant:X8}";
            DEV_Ability.SelectedValue = pk.Ability;

            // with some simple error handling
            var bitNumber = pk.AbilityNumber;
            int abilityIndex = AbilityVerifier.IsValidAbilityBits(bitNumber) ? bitNumber >> 1 : 0;
            if (abilityIndex >= CB_Ability.Items.Count) // sanity check ability count being possible
                abilityIndex = CB_Ability.Items.Count - 1; // last possible index, if out of range

            CB_Ability.SelectedIndex = abilityIndex;
            TB_AbilityNumber.Text = bitNumber.ToString();

            LoadRelearnMoves(pk);
            LoadHandlingTrainer(pk);

            if (pk is IRegionOrigin tr)
                LoadGeolocation(tr);
        }

        private void SaveMisc6(PKM pk)
        {
            pk.EncryptionConstant = Util.GetHexValue(TB_EC.Text);
            if (PIDVerifier.GetTransferEC(pk, out var ec))
                pk.EncryptionConstant = ec;

            pk.AbilityNumber = Util.ToInt32(TB_AbilityNumber.Text);

            SaveRelearnMoves(pk);
            SaveHandlingTrainer(pk);

            if (pk is IRegionOrigin tr)
                SaveGeolocation(tr);
        }

        private void LoadGeolocation(IRegionOrigin pk)
        {
            CB_Country.SelectedValue = (int)pk.Country;
            CB_SubRegion.SelectedValue = (int)pk.Region;
            CB_3DSReg.SelectedValue = (int)pk.ConsoleRegion;
        }

        private void SaveGeolocation(IRegionOrigin pk)
        {
            pk.Country = (byte)WinFormsUtil.GetIndex(CB_Country);
            pk.Region = (byte)WinFormsUtil.GetIndex(CB_SubRegion);
            pk.ConsoleRegion = (byte)WinFormsUtil.GetIndex(CB_3DSReg);
        }

        private void LoadHandlingTrainer(PKM pk)
        {
            var handler = pk.HT_Name;
            int gender = pk.HT_Gender & 1;

            TB_OTt2.Text = handler;
            // Set CT Gender to None if no CT, else set to gender symbol.
            Label_CTGender.Text = string.IsNullOrEmpty(handler) ? string.Empty : gendersymbols[gender];
            Label_CTGender.ForeColor = Draw.GetGenderColor(gender);

            // Indicate who is currently in possession of the PKM
            UpadteHandlingTrainerBackground(pk.CurrentHandler);
        }

        private void UpadteHandlingTrainerBackground(int handler)
        {
            var activeColor = ImageUtil.ChangeOpacity(SpriteUtil.Spriter.Set, 0.5);
            if (handler == 0) // OT
            {
                GB_OT.BackgroundImage = activeColor;
                GB_nOT.BackgroundImage = null;
            }
            else // Handling Trainer
            {
                GB_nOT.BackgroundImage = activeColor;
                GB_OT.BackgroundImage = null;
            }
        }

        private void SaveHandlingTrainer(PKM pk)
        {
            pk.HT_Name = TB_OTt2.Text;
            pk.HT_Gender = PKX.GetGenderFromString(Label_CTGender.Text) & 1;
        }

        private void LoadAbility4(PKM pk)
        {
            var index = GetAbilityIndex4(pk);
            CB_Ability.SelectedIndex = Math.Min(CB_Ability.Items.Count - 1, index);
        }

        private static int GetAbilityIndex4(PKM pk)
        {
            var pi = pk.PersonalInfo;
            int abilityIndex = pi.GetAbilityIndex(pk.Ability);
            if (abilityIndex < 0)
                return 0;
            if (abilityIndex >= 2)
                return 2;

            var abils = pi.Abilities;
            if (abils[0] == abils[1])
                return pk.PIDAbility;
            return abilityIndex;
        }

        private void LoadMisc8(PK8 pk8)
        {
            CB_StatNature.SelectedValue = pk8.StatNature;
            Stats.CB_DynamaxLevel.SelectedIndex = pk8.DynamaxLevel;
            Stats.CHK_Gigantamax.Checked = pk8.CanGigantamax;
            CB_HTLanguage.SelectedValue = pk8.HT_Language;
            TB_HomeTracker.Text = pk8.Tracker.ToString("X16");
            CB_BattleVersion.SelectedValue = pk8.BattleVersion;
        }

        private void SaveMisc8(PK8 pk8)
        {
            pk8.StatNature = WinFormsUtil.GetIndex(CB_StatNature);
            pk8.DynamaxLevel = (byte)Math.Max(0, Stats.CB_DynamaxLevel.SelectedIndex);
            pk8.CanGigantamax = Stats.CHK_Gigantamax.Checked;
            pk8.HT_Language = WinFormsUtil.GetIndex(CB_HTLanguage);
            pk8.BattleVersion = WinFormsUtil.GetIndex(CB_BattleVersion);
        }

        private void LoadMisc8(PB8 pk8)
        {
            CB_StatNature.SelectedValue = pk8.StatNature;
            Stats.CB_DynamaxLevel.SelectedIndex = pk8.DynamaxLevel;
            Stats.CHK_Gigantamax.Checked = pk8.CanGigantamax;
            CB_HTLanguage.SelectedValue = pk8.HT_Language;
            TB_HomeTracker.Text = pk8.Tracker.ToString("X16");
            CB_BattleVersion.SelectedValue = pk8.BattleVersion;
        }

        private void SaveMisc8(PB8 pk8)
        {
            pk8.StatNature = WinFormsUtil.GetIndex(CB_StatNature);
            pk8.DynamaxLevel = (byte)Math.Max(0, Stats.CB_DynamaxLevel.SelectedIndex);
            pk8.CanGigantamax = Stats.CHK_Gigantamax.Checked;
            pk8.HT_Language = WinFormsUtil.GetIndex(CB_HTLanguage);
            pk8.BattleVersion = WinFormsUtil.GetIndex(CB_BattleVersion);
        }

        private void LoadMisc8(PA8 pk8)
        {
            CB_StatNature.SelectedValue = pk8.StatNature;
            Stats.CB_DynamaxLevel.SelectedIndex = pk8.DynamaxLevel;
            Stats.CHK_Gigantamax.Checked = pk8.CanGigantamax;
            CB_HTLanguage.SelectedValue = pk8.HT_Language;
            TB_HomeTracker.Text = pk8.Tracker.ToString("X16");
            CB_BattleVersion.SelectedValue = pk8.BattleVersion;
            Stats.CHK_IsAlpha.Checked = pk8.IsAlpha;
            Stats.CHK_IsNoble.Checked = pk8.IsNoble;
            CB_AlphaMastered.SelectedValue = pk8.AlphaMove;
        }

        private void SaveMisc8(PA8 pk8)
        {
            pk8.StatNature = WinFormsUtil.GetIndex(CB_StatNature);
            pk8.DynamaxLevel = (byte)Math.Max(0, Stats.CB_DynamaxLevel.SelectedIndex);
            pk8.CanGigantamax = Stats.CHK_Gigantamax.Checked;
            pk8.HT_Language = WinFormsUtil.GetIndex(CB_HTLanguage);
            pk8.BattleVersion = WinFormsUtil.GetIndex(CB_BattleVersion);
            pk8.IsAlpha = Stats.CHK_IsAlpha.Checked;
            pk8.IsNoble = Stats.CHK_IsNoble.Checked;
            pk8.AlphaMove = WinFormsUtil.GetIndex(CB_AlphaMastered);
        }
    }
}
