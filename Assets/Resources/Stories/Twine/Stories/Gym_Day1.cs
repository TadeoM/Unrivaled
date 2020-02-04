﻿/*
------------------------------------------------
Generated by Cradle 2.0.1.0
https://github.com/daterre/Cradle

Original file: Gym_Day1.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @Gym_Day1: Cradle.StoryFormats.Harlowe.HarloweStory
{
	#region Variables
	// ---------------

	public class VarDefs: RuntimeVars
	{
		public VarDefs()
		{
			VarDef("Jade", () => this.@Jade, val => this.@Jade = val);
			VarDef("Ava", () => this.@Ava, val => this.@Ava = val);
			VarDef("mysteryAva", () => this.@mysteryAva, val => this.@mysteryAva = val);
			VarDef("characters", () => this.@characters, val => this.@characters = val);
			VarDef("avaMeter", () => this.@avaMeter, val => this.@avaMeter = val);
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
		}

		public StoryVar @Jade;
		public StoryVar @Ava;
		public StoryVar @mysteryAva;
		public StoryVar @characters;
		public StoryVar @avaMeter;
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

	@Gym_Day1()
	{
		this.StartPassage = "Gym-Day1-Init";

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
	}

	// ---------------
	#endregion

	// .............
	// #1: Gym-Day1-Init

	void passage1_Init()
	{
		this.Passages[@"Gym-Day1-Init"] = new StoryPassage(@"Gym-Day1-Init", new string[]{  }, passage1_Main);
	}

	IStoryThread passage1_Main()
	{
		Vars.Jade  = "Jade";
		Vars.Ava  = "Ava";
		Vars.mysteryAva  = "???";
		Vars.characters  = macros1.a(Vars.Jade, Vars.Ava, Vars.mysteryAva);
		Vars.avaMeter  = 5;
		Vars.leftCharacter  = Vars.Jade;
		Vars.rightCharacter  = StoryVar.Empty;
		Vars.jadeHappy  = false;
		Vars.jadeSad  = false;
		Vars.jadeAngry  = true;
		Vars.jadeNeutral  = false;
		Vars.avaHappy  = false;
		Vars.avaSad  = false;
		Vars.avaAngry  = false;
		Vars.avaNeutral  = true;
		yield return text("Jade is working out at the gym. She's about to make her Division 3 debut soon. She was told she'd get to meet a woman named Ava some time before the show to understand the business a bit better, but she hasn't shown up. Jade continues her reps.");
		yield return link("<br>$Jade: *grunt* Geez, these weights...", "Gym-Day1", null);
		yield break;
	}


	// .............
	// #2: Gym-Day1

	void passage2_Init()
	{
		this.Passages[@"Gym-Day1"] = new StoryPassage(@"Gym-Day1", new string[]{  }, passage2_Main);
	}

	IStoryThread passage2_Main()
	{
		yield return text(Vars.mysteryAva);
		yield return text(": Yo, are you okay? Looks like you're struggling.");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("<br>$Jade: I'm *grunt* I'm fine.", "Gym-Day1-1-AB", null);
		yield return lineBreak();
		yield return link("<br>$Jade: Yeah, just help me finish this set.", "Gym-Day1-1-AB", null);
		yield return lineBreak();
		yield return link("<br>$Jade: ...", "Gym-Day1-1-C", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #3: Gym-Day1-1-AB

	void passage3_Init()
	{
		this.Passages[@"Gym-Day1-1-AB"] = new StoryPassage(@"Gym-Day1-1-AB", new string[]{  }, passage3_Main);
	}

	IStoryThread passage3_Main()
	{
		yield return text(Vars.mysteryAva);
		yield return text(": Don't worry, I got you!");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-2-AB", null);
		yield break;
	}


	// .............
	// #4: Gym-Day1-1-C

	void passage4_Init()
	{
		this.Passages[@"Gym-Day1-1-C"] = new StoryPassage(@"Gym-Day1-1-C", new string[]{  }, passage4_Main);
	}

	IStoryThread passage4_Main()
	{
		yield return text("???: Man, you’re just going to ignore your mentor like that?");
		Vars.avaMeter  = Vars.avaMeter - 5;
		yield return lineBreak();
		yield return link("v", "Gym-Day1-2-C", null);
		yield break;
	}


	// .............
	// #5: Gym-Day1-2-AB

	void passage5_Init()
	{
		this.Passages[@"Gym-Day1-2-AB"] = new StoryPassage(@"Gym-Day1-2-AB", new string[]{  }, passage5_Main);
	}

	IStoryThread passage5_Main()
	{
		yield return text(Vars.Jade);
		yield return text(" finishes her set with the help of ");
		yield return text(Vars.mysteryAva);
		yield return text(" and turns around.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-3-AB", null);
		yield break;
	}


	// .............
	// #6: Gym-Day1-3-AB

	void passage6_Init()
	{
		this.Passages[@"Gym-Day1-3-AB"] = new StoryPassage(@"Gym-Day1-3-AB", new string[]{  }, passage6_Main);
	}

	IStoryThread passage6_Main()
	{
		yield return text(Vars.Jade);
		yield return text(":  Wait, you’re—You’re Ava! Sweet Spike! I’ve watched you a lot when you were in Division 1! ");
		Vars.jadeAngry  = false;
		Vars.jadeHappy  = true;
		yield return lineBreak();
		yield return link("v", "Gym-Day1-4-AB", null);
		yield break;
	}


	// .............
	// #7: Gym-Day1-4-AB

	void passage7_Init()
	{
		this.Passages[@"Gym-Day1-4-AB"] = new StoryPassage(@"Gym-Day1-4-AB", new string[]{  }, passage7_Main);
	}

	IStoryThread passage7_Main()
	{
		Vars.rightCharacter  = Vars.Ava;
		Vars.avaNeutral  = false;
		Vars.avaHappy  = true;
		yield return text("Ava: That's me! Thank you for being a fan! Your name is Jade, right?");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-5", null);
		yield break;
	}


	// .............
	// #8: Gym-Day1-5

	void passage8_Init()
	{
		this.Passages[@"Gym-Day1-5"] = new StoryPassage(@"Gym-Day1-5", new string[]{  }, passage8_Main);
	}

	IStoryThread passage8_Main()
	{
		yield return text("Jade: Yeah, Jade! Jade Valentine.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-6", null);
		yield break;
	}


	// .............
	// #9: Gym-Day1-6

	void passage9_Init()
	{
		this.Passages[@"Gym-Day1-6"] = new StoryPassage(@"Gym-Day1-6", new string[]{  }, passage9_Main);
	}

	IStoryThread passage9_Main()
	{
		yield return text("Ava: Awesome! That’s a cool name. Sorry about not showing up before, had to go to the doctor for some check ups before Monday.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-7", null);
		yield break;
	}


	// .............
	// #10: Gym-Day1-7

	void passage10_Init()
	{
		this.Passages[@"Gym-Day1-7"] = new StoryPassage(@"Gym-Day1-7", new string[]{  }, passage10_Main);
	}

	IStoryThread passage10_Main()
	{
		yield return text("Jade: Your leg, right? I heard you got a leg injury early last year... ");
		Vars.jadeHappy  = false;
		Vars.jadeSad  = true;
		yield return lineBreak();
		yield return link("v", "Gym-Day1-8", null);
		yield break;
	}


	// .............
	// #11: Gym-Day1-8

	void passage11_Init()
	{
		this.Passages[@"Gym-Day1-8"] = new StoryPassage(@"Gym-Day1-8", new string[]{  }, passage11_Main);
	}

	IStoryThread passage11_Main()
	{
		Vars.avaHappy  = false;
		Vars.avaNeutral  = true;
		yield return text("Ava: Yep, messed up a landing. It should be pretty much normal by now though.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-9", null);
		yield break;
	}


	// .............
	// #12: Gym-Day1-9

	void passage12_Init()
	{
		this.Passages[@"Gym-Day1-9"] = new StoryPassage(@"Gym-Day1-9", new string[]{  }, passage12_Main);
	}

	IStoryThread passage12_Main()
	{
		yield return text("Ava: Man, it was rough being away. All that rehab and stuff. Not fun. Now, AOW put me back in Division 3.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-10", null);
		yield break;
	}


	// .............
	// #13: Gym-Day1-10

	void passage13_Init()
	{
		this.Passages[@"Gym-Day1-10"] = new StoryPassage(@"Gym-Day1-10", new string[]{  }, passage13_Main);
	}

	IStoryThread passage13_Main()
	{
		Vars.jadeSad  = false;
		Vars.jadeNeutral  = true;
		yield return text("Jade: Is there anything like, majorly wrong with All Out Wrestling D3? ");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-11", null);
		yield break;
	}


	// .............
	// #14: Gym-Day1-11

	void passage14_Init()
	{
		this.Passages[@"Gym-Day1-11"] = new StoryPassage(@"Gym-Day1-11", new string[]{  }, passage14_Main);
	}

	IStoryThread passage14_Main()
	{
		yield return text("Jade: I never got to watch any of it since it only stays in Laurentia.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-12", null);
		yield break;
	}


	// .............
	// #15: Gym-Day1-12

	void passage15_Init()
	{
		this.Passages[@"Gym-Day1-12"] = new StoryPassage(@"Gym-Day1-12", new string[]{  }, passage15_Main);
	}

	IStoryThread passage15_Main()
	{
		yield return text("Ava: I mean, not really. Less money. Less exposure. That’s to be expected though. It’s only one city, even if that city is massive.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-13", null);
		yield break;
	}


	// .............
	// #16: Gym-Day1-13

	void passage16_Init()
	{
		this.Passages[@"Gym-Day1-13"] = new StoryPassage(@"Gym-Day1-13", new string[]{  }, passage16_Main);
	}

	IStoryThread passage16_Main()
	{
		Vars.avaNeutral  = false;
		Vars.avaHappy  = true;
		yield return text("Ava: Anyway...");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-14", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #17: Gym-Day1-14

	void passage17_Init()
	{
		this.Passages[@"Gym-Day1-14"] = new StoryPassage(@"Gym-Day1-14", new string[]{  }, passage17_Main);
	}

	IStoryThread passage17_Main()
	{
		yield return text("Ava: Are you done working out??");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-15", null);
		yield break;
	}


	// .............
	// #18: Gym-Day1-15

	void passage18_Init()
	{
		this.Passages[@"Gym-Day1-15"] = new StoryPassage(@"Gym-Day1-15", new string[]{  }, passage18_Main);
	}

	IStoryThread passage18_Main()
	{
		yield return text("Jade: I mean-- Not particularly.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-16", null);
		yield break;
	}


	// .............
	// #19: Gym-Day1-16

	void passage19_Init()
	{
		this.Passages[@"Gym-Day1-16"] = new StoryPassage(@"Gym-Day1-16", new string[]{  }, passage19_Main);
	}

	IStoryThread passage19_Main()
	{
		yield return text("Ava: Gah, you’re done working out. Wanna go get something to eat? There’s a café nearby.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-17", null);
		yield break;
	}


	// .............
	// #20: Gym-Day1-17

	void passage20_Init()
	{
		this.Passages[@"Gym-Day1-17"] = new StoryPassage(@"Gym-Day1-17", new string[]{  }, passage20_Main);
	}

	IStoryThread passage20_Main()
	{
		yield return text("Jade: Well, I don’t know…");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-18", null);
		yield break;
	}


	// .............
	// #21: Gym-Day1-18

	void passage21_Init()
	{
		this.Passages[@"Gym-Day1-18"] = new StoryPassage(@"Gym-Day1-18", new string[]{  }, passage21_Main);
	}

	IStoryThread passage21_Main()
	{
		yield return text("Ava: I’m buying~");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-19", null);
		yield break;
	}


	// .............
	// #22: Gym-Day1-19

	void passage22_Init()
	{
		this.Passages[@"Gym-Day1-19"] = new StoryPassage(@"Gym-Day1-19", new string[]{  }, passage22_Main);
	}

	IStoryThread passage22_Main()
	{
		Vars.jadeNeutral  = false;
		Vars.jadeHappy  = true;
		yield return text("Jade: Well, in that case...");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-20", null);
		yield break;
	}


	// .............
	// #23: Gym-Day1-20

	void passage23_Init()
	{
		this.Passages[@"Gym-Day1-20"] = new StoryPassage(@"Gym-Day1-20", new string[]{  }, passage23_Main);
	}

	IStoryThread passage23_Main()
	{
		yield return text("Ava: Awesome! Let’s go! ");
		Vars.avaMeter  = Vars.avaMeter + 5;
		yield return lineBreak();
		yield return link("v", "Gym-Day1-End", null);
		yield break;
	}


	// .............
	// #24: Gym-Day1-End

	void passage24_Init()
	{
		this.Passages[@"Gym-Day1-End"] = new StoryPassage(@"Gym-Day1-End", new string[]{  }, passage24_Main);
	}

	IStoryThread passage24_Main()
	{
		yield return text("Double-click this passage to edit it.");
		yield break;
	}


	// .............
	// #25: Gym-Day1-2-C

	void passage25_Init()
	{
		this.Passages[@"Gym-Day1-2-C"] = new StoryPassage(@"Gym-Day1-2-C", new string[]{  }, passage25_Main);
	}

	IStoryThread passage25_Main()
	{
		yield return text("Jade stops her set immediately and turns around.");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-3-C", null);
		yield break;
	}


	// .............
	// #26: Gym-Day1-3-C

	void passage26_Init()
	{
		this.Passages[@"Gym-Day1-3-C"] = new StoryPassage(@"Gym-Day1-3-C", new string[]{  }, passage26_Main);
	}

	IStoryThread passage26_Main()
	{
		Vars.jadeAngry  = false;
		Vars.jadeSad  = true;
		yield return text("Jade: Wait, ");
		using (Group("strong", true)) {
			yield return text("you’re");
		}
		yield return text(" Ava? I’m sorry! I’m actually a big fan of your Division 1 work! I just didn’t know who it was…");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-4-C", null);
		yield break;
	}


	// .............
	// #27: Gym-Day1-4-C

	void passage27_Init()
	{
		this.Passages[@"Gym-Day1-4-C"] = new StoryPassage(@"Gym-Day1-4-C", new string[]{  }, passage27_Main);
	}

	IStoryThread passage27_Main()
	{
		Vars.rightCharacter  = Vars.Ava;
		Vars.avaNeutral  = false;
		Vars.avaHappy  = true;
		yield return text("Ava: Ah, don’t worry about it! Truth be told, I haven’t been out much. Jade, right?");
		yield return lineBreak();
		yield return link("v", "Gym-Day1-5", null);
		yield return text(" ");
		yield break;
	}


}