﻿/*
------------------------------------------------
Generated by Cradle 2.0.1.0
https://github.com/daterre/Cradle

Original file: Cafe_DayOne_Ava.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @Cafe_DayOne_Ava: Cradle.StoryFormats.Harlowe.HarloweStory
{
	#region Variables
	// ---------------

	public class VarDefs: RuntimeVars
	{
		public VarDefs()
		{
			VarDef("Jade", () => this.@Jade, val => this.@Jade = val);
			VarDef("Ava", () => this.@Ava, val => this.@Ava = val);
			VarDef("characters", () => this.@characters, val => this.@characters = val);
			VarDef("avaMeter", () => this.@avaMeter, val => this.@avaMeter = val);
			VarDef("claudioMeter", () => this.@claudioMeter, val => this.@claudioMeter = val);
			VarDef("lutherMeter", () => this.@lutherMeter, val => this.@lutherMeter = val);
			VarDef("leftCharacter", () => this.@leftCharacter, val => this.@leftCharacter = val);
			VarDef("rightCharacter", () => this.@rightCharacter, val => this.@rightCharacter = val);
			VarDef("jadeHappy", () => this.@jadeHappy, val => this.@jadeHappy = val);
			VarDef("jadeSad", () => this.@jadeSad, val => this.@jadeSad = val);
			VarDef("jadeAngry", () => this.@jadeAngry, val => this.@jadeAngry = val);
			VarDef("jadeNeutral", () => this.@jadeNeutral, val => this.@jadeNeutral = val);
			VarDef("avaHappy", () => this.@avaHappy, val => this.@avaHappy = val);
			VarDef("avaSad", () => this.@avaSad, val => this.@avaSad = val);
			VarDef("avaAngry", () => this.@avaAngry, val => this.@avaAngry = val);
			VarDef("avaNeutral", () => this.@avaNeutral, val => this.@avaNeutral = val);
			VarDef("backToGym", () => this.@backToGym, val => this.@backToGym = val);
			VarDef("goingToJadeRoom", () => this.@goingToJadeRoom, val => this.@goingToJadeRoom = val);
		}

		public StoryVar @Jade;
		public StoryVar @Ava;
		public StoryVar @characters;
		public StoryVar @avaMeter;
		public StoryVar @claudioMeter;
		public StoryVar @lutherMeter;
		public StoryVar @leftCharacter;
		public StoryVar @rightCharacter;
		public StoryVar @jadeHappy;
		public StoryVar @jadeSad;
		public StoryVar @jadeAngry;
		public StoryVar @jadeNeutral;
		public StoryVar @avaHappy;
		public StoryVar @avaSad;
		public StoryVar @avaAngry;
		public StoryVar @avaNeutral;
		public StoryVar @backToGym;
		public StoryVar @goingToJadeRoom;
	}

	public new VarDefs Vars
	{
		get { return (VarDefs) base.Vars; }
	}

	// ---------------
	#endregion

	#region Initialization
	// ---------------

	public readonly Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros macros1;

	@Cafe_DayOne_Ava()
	{
		this.StartPassage = "CafeDayOne_Init";

		base.Vars = new VarDefs() { Story = this, StrictMode = true };

		macros1 = new Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros() { Story = this };

		base.Init();
		passage1_Init();
		passage2_Init();
		passage3_Init();
		passage4_Init();
		passage5_Init();
		passage6_Init();
		passage7_Init();
		passage8_Init();
		passage9_Init();
		passage10_Init();
		passage11_Init();
		passage12_Init();
		passage13_Init();
		passage14_Init();
		passage15_Init();
		passage16_Init();
		passage17_Init();
		passage18_Init();
		passage19_Init();
		passage20_Init();
		passage21_Init();
		passage22_Init();
		passage23_Init();
		passage24_Init();
		passage25_Init();
		passage26_Init();
		passage27_Init();
		passage28_Init();
		passage29_Init();
		passage30_Init();
		passage31_Init();
		passage32_Init();
		passage33_Init();
		passage34_Init();
		passage35_Init();
		passage36_Init();
		passage37_Init();
		passage38_Init();
		passage39_Init();
		passage40_Init();
	}

	// ---------------
	#endregion

	// .............
	// #1: CafeDayOne_Init

	void passage1_Init()
	{
		this.Passages[@"CafeDayOne_Init"] = new StoryPassage(@"CafeDayOne_Init", new string[]{  }, passage1_Main);
	}

	IStoryThread passage1_Main()
	{
		Vars.Jade  = "Jade";
		yield return lineBreak();
		Vars.Ava  = "Ava";
		yield return lineBreak();
		yield return lineBreak();
		Vars.characters  = macros1.a(Vars.Jade, Vars.Ava);
		yield return lineBreak();
		yield return lineBreak();
		Vars.avaMeter  = 10;
		yield return lineBreak();
		Vars.claudioMeter  = 0;
		yield return lineBreak();
		Vars.lutherMeter  = 0;
		yield return lineBreak();
		yield return lineBreak();
		Vars.leftCharacter  = Vars.Jade;
		yield return lineBreak();
		Vars.rightCharacter  = Vars.Ava;
		yield return lineBreak();
		yield return lineBreak();
		Vars.jadeHappy  = false;
		yield return lineBreak();
		Vars.jadeSad  = false;
		yield return lineBreak();
		Vars.jadeAngry  = false;
		yield return lineBreak();
		Vars.jadeNeutral  = true;
		yield return lineBreak();
		yield return lineBreak();
		Vars.avaHappy  = false;
		yield return lineBreak();
		Vars.avaSad  = false;
		yield return lineBreak();
		Vars.avaAngry  = false;
		yield return lineBreak();
		Vars.avaNeutral  = true;
		yield return lineBreak();
		yield return lineBreak();
		Vars.backToGym  = false;
		yield return lineBreak();
		Vars.goingToJadeRoom  = false;
		yield return lineBreak();
		yield return lineBreak();
		yield return abort(goToPassage: "CafeDayOne01");
		yield break;
	}


	// .............
	// #2: CafeDayOne01

	void passage2_Init()
	{
		this.Passages[@"CafeDayOne01"] = new StoryPassage(@"CafeDayOne01", new string[]{  }, passage2_Main);
	}

	IStoryThread passage2_Main()
	{
		yield return text("Ava: So, Jade, tell me about your character in the ring. What are you like?");
		yield return lineBreak();
		yield return link("v", "CafeDayOne02", null);
		yield break;
	}


	// .............
	// #3: CafeDayOne02

	void passage3_Init()
	{
		this.Passages[@"CafeDayOne02"] = new StoryPassage(@"CafeDayOne02", new string[]{  }, passage3_Main);
	}

	IStoryThread passage3_Main()
	{
		yield return text("Jade: Well…");
		yield return lineBreak();
		yield return link("<br>I'm more of a rebel type.", "CafeDayOne03-A", null);
		yield return lineBreak();
		yield return link("<br>I'm like the calm, silent, strong type.", "CafeDayOne03-B", null);
		yield return lineBreak();
		yield return link("<br>I think I'm cute, but a badass at the same time.", "CafeDayOne03-C", null);
		yield break;
	}


	// .............
	// #4: CafeDayOne03-A

	void passage4_Init()
	{
		this.Passages[@"CafeDayOne03-A"] = new StoryPassage(@"CafeDayOne03-A", new string[]{  }, passage4_Main);
	}

	IStoryThread passage4_Main()
	{
		Vars.claudioMeter  = Vars.claudioMeter + 5;
		yield return text("Ava: Oh, that’s kind of like Claudio. I think you two would get along!");
		yield return lineBreak();
		yield return link("v", "CafeDayOne04", null);
		yield break;
	}


	// .............
	// #5: CafeDayOne03-B

	void passage5_Init()
	{
		this.Passages[@"CafeDayOne03-B"] = new StoryPassage(@"CafeDayOne03-B", new string[]{  }, passage5_Main);
	}

	IStoryThread passage5_Main()
	{
		Vars.lutherMeter  = Vars.lutherMeter + 5;
		yield return text("Ava: Oh wow, that’s kind of like the Division 1 champ right now, Luther. Looks like you got being the D-1 champ coming in your future if you’re taking after him!");
		yield return lineBreak();
		yield return link("v", "CafeDayOne04", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #6: CafeDayOne03-C

	void passage6_Init()
	{
		this.Passages[@"CafeDayOne03-C"] = new StoryPassage(@"CafeDayOne03-C", new string[]{  }, passage6_Main);
	}

	IStoryThread passage6_Main()
	{
		Vars.avaNeutral  = false;
		Vars.avaHappy  = true;
		yield return text("Ava: Hehe, that’s kind of like me!");
		Vars.avaMeter  = Vars.avaMeter +5;
		yield return lineBreak();
		yield return link("v", "CafeDayOne04", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #7: CafeDayOne04

	void passage7_Init()
	{
		this.Passages[@"CafeDayOne04"] = new StoryPassage(@"CafeDayOne04", new string[]{  }, passage7_Main);
	}

	IStoryThread passage7_Main()
	{
		Vars.jadeHappy  = true;
		Vars.jadeNeutral  = false;
		yield return text("Jade: Oh, uh, nice!");
		yield return lineBreak();
		yield return link("v", "CafeDayOne05", null);
		yield break;
	}


	// .............
	// #8: CafeDayOne05

	void passage8_Init()
	{
		this.Passages[@"CafeDayOne05"] = new StoryPassage(@"CafeDayOne05", new string[]{  }, passage8_Main);
	}

	IStoryThread passage8_Main()
	{
		yield return text("A nervous young man approaches Ava at the table.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne06", null);
		yield break;
	}


	// .............
	// #9: CafeDayOne06

	void passage9_Init()
	{
		this.Passages[@"CafeDayOne06"] = new StoryPassage(@"CafeDayOne06", new string[]{  }, passage9_Main);
	}

	IStoryThread passage9_Main()
	{
		Vars.avaHappy  = false;
		Vars.avaNeutral  = true;
		yield return text("Young Man: Ms… Ms—Ms. Sweet Spike! C-C-Can I get an a-autograph??? My hat! P-Please autograph my hat!");
		Vars.jadeHappy  = false;
		Vars.jadeNeutral  = true;
		yield return lineBreak();
		yield return link("v", "CafeDayOne07", null);
		yield break;
	}


	// .............
	// #10: CafeDayOne07

	void passage10_Init()
	{
		this.Passages[@"CafeDayOne07"] = new StoryPassage(@"CafeDayOne07", new string[]{  }, passage10_Main);
	}

	IStoryThread passage10_Main()
	{
		Vars.avaNeutral  = false;
		Vars.avaAngry  = true;
		yield return text("Ava: Eh? You think I would soil my name by putting it on some random loser’s hat?");
		yield return lineBreak();
		yield return link("v", "CafeDayOne08", null);
		yield break;
	}


	// .............
	// #11: CafeDayOne08

	void passage11_Init()
	{
		this.Passages[@"CafeDayOne08"] = new StoryPassage(@"CafeDayOne08", new string[]{  }, passage11_Main);
	}

	IStoryThread passage11_Main()
	{
		yield return text("Ava: Tch, tell you what, if I see you at the show, then maybe I’ll think about looking in your direction. ");
		using (Group("strong", true)) {
			yield return text("Maybe");
		}
		yield return text(".");
		yield return lineBreak();
		yield return link("v", "CafeDayOne09", null);
		yield break;
	}


	// .............
	// #12: CafeDayOne09

	void passage12_Init()
	{
		this.Passages[@"CafeDayOne09"] = new StoryPassage(@"CafeDayOne09", new string[]{  }, passage12_Main);
	}

	IStoryThread passage12_Main()
	{
		yield return text("Ava: For now, get the fuck out of my sight.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne10", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #13: CafeDayOne10

	void passage13_Init()
	{
		this.Passages[@"CafeDayOne10"] = new StoryPassage(@"CafeDayOne10", new string[]{  }, passage13_Main);
	}

	IStoryThread passage13_Main()
	{
		yield return text("Young Man: Th-THANK YOU SWEET SPIKE! I LOVE YOU!");
		yield return lineBreak();
		yield return link("v", "CafeDayOne11", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #14: CafeDayOne11

	void passage14_Init()
	{
		this.Passages[@"CafeDayOne11"] = new StoryPassage(@"CafeDayOne11", new string[]{  }, passage14_Main);
	}

	IStoryThread passage14_Main()
	{
		yield return text("The Young Man leaves the cafe. He looks quite excited.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne12", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #15: CafeDayOne12

	void passage15_Init()
	{
		this.Passages[@"CafeDayOne12"] = new StoryPassage(@"CafeDayOne12", new string[]{  }, passage15_Main);
	}

	IStoryThread passage15_Main()
	{
		Vars.jadeNeutral  = false;
		Vars.jadeSad  = true;
		yield return text("Jade: Woah, that was pretty harsh. I thought you were… you know, sweet? You turned into a bad guy?");
		yield return lineBreak();
		yield return link("v", "CafeDayOne13", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #16: CafeDayOne13

	void passage16_Init()
	{
		this.Passages[@"CafeDayOne13"] = new StoryPassage(@"CafeDayOne13", new string[]{  }, passage16_Main);
	}

	IStoryThread passage16_Main()
	{
		Vars.avaAngry  = false;
		Vars.avaNeutral  = true;
		yield return text("Ava: Yeah, I was sweeter before the injury, but AOW wants me to be heel for my return to D-1.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne14", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #17: CafeDayOne14

	void passage17_Init()
	{
		this.Passages[@"CafeDayOne14"] = new StoryPassage(@"CafeDayOne14", new string[]{  }, passage17_Main);
	}

	IStoryThread passage17_Main()
	{
		yield return text("Ava: Makes sense you didn’t know I was heel because you couldn't watch D-3. But yeah, I’m a heel now.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne15", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #18: CafeDayOne15

	void passage18_Init()
	{
		this.Passages[@"CafeDayOne15"] = new StoryPassage(@"CafeDayOne15", new string[]{  }, passage18_Main);
	}

	IStoryThread passage18_Main()
	{
		Vars.jadeSad  = false;
		Vars.jadeNeutral  = true;
		yield return lineBreak();
		yield return link("<br>Heel? Like, the bad guy, right? ", "CafeDayOne16-A", null);
		yield return lineBreak();
		yield return link("<br>So, the bad guy is called a heel around here?", "CafeDayOne16-B", null);
		yield return lineBreak();
		yield return link("<br>You’re supposed to boo the heel, right? The bad guy.", "CafeDayOne16-C", null);
		yield break;
	}


	// .............
	// #19: CafeDayOne16-A

	void passage19_Init()
	{
		this.Passages[@"CafeDayOne16-A"] = new StoryPassage(@"CafeDayOne16-A", new string[]{  }, passage19_Main);
	}

	IStoryThread passage19_Main()
	{
		yield return text("Jade: Heel? Like, the bad guy, right? Sorry, still getting used to the terminology.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne17", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #20: CafeDayOne16-B

	void passage20_Init()
	{
		this.Passages[@"CafeDayOne16-B"] = new StoryPassage(@"CafeDayOne16-B", new string[]{  }, passage20_Main);
	}

	IStoryThread passage20_Main()
	{
		yield return text("Jade: So, the bad guy is called a heel around here? Sorry, still getting used to the terminology.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne17", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #21: CafeDayOne16-C

	void passage21_Init()
	{
		this.Passages[@"CafeDayOne16-C"] = new StoryPassage(@"CafeDayOne16-C", new string[]{  }, passage21_Main);
	}

	IStoryThread passage21_Main()
	{
		yield return text("Jade: You’re supposed to boo the heel, right? The bad guy. Sorry, still getting used to the terminology.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne17", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #22: CafeDayOne17

	void passage22_Init()
	{
		this.Passages[@"CafeDayOne17"] = new StoryPassage(@"CafeDayOne17", new string[]{  }, passage22_Main);
	}

	IStoryThread passage22_Main()
	{
		yield return text("Jade: Back from where I'm from, Cascadia, we never really used the terms.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne18", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #23: CafeDayOne18

	void passage23_Init()
	{
		this.Passages[@"CafeDayOne18"] = new StoryPassage(@"CafeDayOne18", new string[]{  }, passage23_Main);
	}

	IStoryThread passage23_Main()
	{
		yield return text("Jade: We just kind of went with the fans on who they wanted to cheer or boo that night.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne19", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #24: CafeDayOne19

	void passage24_Init()
	{
		this.Passages[@"CafeDayOne19"] = new StoryPassage(@"CafeDayOne19", new string[]{  }, passage24_Main);
	}

	IStoryThread passage24_Main()
	{
		yield return text("Jade: Is AOW that strict about that kind of thing? Always being your character and everything?");
		yield return lineBreak();
		yield return link("v", "CafeDayOne20", null);
		yield break;
	}


	// .............
	// #25: CafeDayOne20

	void passage25_Init()
	{
		this.Passages[@"CafeDayOne20"] = new StoryPassage(@"CafeDayOne20", new string[]{  }, passage25_Main);
	}

	IStoryThread passage25_Main()
	{
		yield return text("Ava: Well, not so much in D-1, but in the lower divisions? Absolutely. Especially in D-3.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne21", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #26: CafeDayOne21

	void passage26_Init()
	{
		this.Passages[@"CafeDayOne21"] = new StoryPassage(@"CafeDayOne21", new string[]{  }, passage26_Main);
	}

	IStoryThread passage26_Main()
	{
		yield return text("Ava: People really want to believe that whatever they’re watching is real. Even if it’s painfully obvious that it’s not.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne22", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #27: CafeDayOne22

	void passage27_Init()
	{
		this.Passages[@"CafeDayOne22"] = new StoryPassage(@"CafeDayOne22", new string[]{  }, passage27_Main);
	}

	IStoryThread passage27_Main()
	{
		yield return text("Ava: So, outside of the ring, you gotta make sure to act the same.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne23", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #28: CafeDayOne23

	void passage28_Init()
	{
		this.Passages[@"CafeDayOne23"] = new StoryPassage(@"CafeDayOne23", new string[]{  }, passage28_Main);
	}

	IStoryThread passage28_Main()
	{
		yield return text("Ava: You know, kayfabe, presenting everything that happening in-ring like it’s real? Have to keep up the act in and outside of the ring.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne24", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #29: CafeDayOne24

	void passage29_Init()
	{
		this.Passages[@"CafeDayOne24"] = new StoryPassage(@"CafeDayOne24", new string[]{  }, passage29_Main);
	}

	IStoryThread passage29_Main()
	{
		yield return text("Jade: I guess I never thought much of it. There wasn’t much room to believe what was happening was real where I was.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne25", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #30: CafeDayOne25

	void passage30_Init()
	{
		this.Passages[@"CafeDayOne25"] = new StoryPassage(@"CafeDayOne25", new string[]{  }, passage30_Main);
	}

	IStoryThread passage30_Main()
	{
		yield return text("Jade: A bunch of the other wrestlers were less than half organic.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne26", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #31: CafeDayOne26

	void passage31_Init()
	{
		this.Passages[@"CafeDayOne26"] = new StoryPassage(@"CafeDayOne26", new string[]{  }, passage31_Main);
	}

	IStoryThread passage31_Main()
	{
		Vars.avaNeutral  = false;
		Vars.avaHappy  = true;
		yield return text("Ava: That does remind me, you have to tell me what made you want to stay full organic some time. ");
		yield return lineBreak();
		yield return link("v", "CafeDayOne27", null);
		yield break;
	}


	// .............
	// #32: CafeDayOne27

	void passage32_Init()
	{
		this.Passages[@"CafeDayOne27"] = new StoryPassage(@"CafeDayOne27", new string[]{  }, passage32_Main);
	}

	IStoryThread passage32_Main()
	{
		yield return text("Jade: Maybe... It's a long story.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne28", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #33: CafeDayOne28

	void passage33_Init()
	{
		this.Passages[@"CafeDayOne28"] = new StoryPassage(@"CafeDayOne28", new string[]{  }, passage33_Main);
	}

	IStoryThread passage33_Main()
	{
		yield return text("Ava: All good! We can trade stories whenever you want. ");
		Vars.avaMeter  = Vars.avaMeter + 5;
		yield return lineBreak();
		yield return link("v", "CafeDayOne29", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #34: CafeDayOne29

	void passage34_Init()
	{
		this.Passages[@"CafeDayOne29"] = new StoryPassage(@"CafeDayOne29", new string[]{  }, passage34_Main);
	}

	IStoryThread passage34_Main()
	{
		Vars.avaHappy  = false;
		Vars.avaNeutral  = true;
		yield return text("Ava: Anyway, you have a match coming up Monday right? We should definitely talk about it for a bit.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne30", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #35: CafeDayOne30

	void passage35_Init()
	{
		this.Passages[@"CafeDayOne30"] = new StoryPassage(@"CafeDayOne30", new string[]{  }, passage35_Main);
	}

	IStoryThread passage35_Main()
	{
		yield return text("Jade: Oh, okay, sure!");
		yield return lineBreak();
		yield return link("v", "CafeDayOne31", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #36: CafeDayOne31

	void passage36_Init()
	{
		this.Passages[@"CafeDayOne31"] = new StoryPassage(@"CafeDayOne31", new string[]{  }, passage36_Main);
	}

	IStoryThread passage36_Main()
	{
		yield return text("Ava: Silly, can’t be here, we need somewhere super secret! Like the AOW gym or something.");
		yield return lineBreak();
		yield return link("v", "CafeDayOne32", null);
		yield break;
	}


	// .............
	// #37: CafeDayOne32

	void passage37_Init()
	{
		this.Passages[@"CafeDayOne32"] = new StoryPassage(@"CafeDayOne32", new string[]{  }, passage37_Main);
	}

	IStoryThread passage37_Main()
	{
		yield return link("<br>Alright, let's head back to the gym then.", "CafeDayOne33-A", null);
		yield return lineBreak();
		yield return link("<br>Go back to the gym? I'd rather just relax at home...", "CafeDayOne33-B", null);
		yield break;
	}


	// .............
	// #38: CafeDayOne33-A

	void passage38_Init()
	{
		this.Passages[@"CafeDayOne33-A"] = new StoryPassage(@"CafeDayOne33-A", new string[]{  }, passage38_Main);
	}

	IStoryThread passage38_Main()
	{
		Vars.avaNeutral  = false;
		Vars.avaHappy  = true;
		yield return text("Ava: Awesome! ");
		Vars.avaMeter  = Vars.avaMeter + 5;
		Vars.backToGym  = true;
		yield return lineBreak();
		yield return link("v", "CafeDayOne-End", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #39: CafeDayOne33-B

	void passage39_Init()
	{
		this.Passages[@"CafeDayOne33-B"] = new StoryPassage(@"CafeDayOne33-B", new string[]{  }, passage39_Main);
	}

	IStoryThread passage39_Main()
	{
		Vars.goingToJadeRoom  = true;
		Vars.avaNeutral  = false;
		Vars.avaSad  = true;
		yield return text("Ava: Awww, that's okay. Rest up! I'll text you tonight then.");
		Vars.avaMeter  = Vars.avaMeter - 5;
		yield return lineBreak();
		yield return link("v", "CafeDayOne-End", null);
		yield return text(" ");
		yield break;
	}


	// .............
	// #40: CafeDayOne-End

	void passage40_Init()
	{
		this.Passages[@"CafeDayOne-End"] = new StoryPassage(@"CafeDayOne-End", new string[]{  }, passage40_Main);
	}

	IStoryThread passage40_Main()
	{
		yield return text("Double-click this passage to edit it.");
		yield break;
	}


}