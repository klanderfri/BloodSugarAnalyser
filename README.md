# BloodSugarAnalyser
A Windows application for analysing Dexcom Clarity blood sugar export files.

This program analyses exported files from the Dexcom Clarity web application and compiles information and values not provided by Dexcom Clarity. For example, it calculates your EBS value.

## Excessive Blood Sugar (EBS)
### What is EBS?
When you use your Dexcom CGM, you choose a target range you want to keep your blood sugar within. 4 to 10 mmol/L is a common target range. Sometimes your blood sugar exceeds your top target limit. The EBS is the area between your top glucose limit and your actual glucose values for the time when the top target limit is exceeded.

### Why is EBS useful?
In Dexcom Clarity you can see how much of your time you spent exceeding your top target limit, but you can't see with how much. Two hours at 16 mmol/L counts as much as two hours at 10,1 mmol/L, even though the former is much worse. EBS on the other hand will show you a value of 12 hmmol/L (milli-mole hours per liter, an unintuitive unit for area in a 2-dimensional system with glucose and time as axes) for the first high and 0,2 hmmol/L for the second high, indicating how bad the highs really were.

## How to download Clarity export files
*Disclaimer! Dexcom Clarity is an external application I have no control nor any responsibility for. Ask them, not me, for help if you fail to download the export files.*
1. Go to https://clarity.dexcom.eu/
2. Select "Dexcom Clarity for Home Users".
3. Log in.
4. Click on "Reports" in the topmost menu.
5. Click on "Export".
6. Select the preferred time interval and click "Export".
7. An export file is downloaded. It has a name like "CLARITY_Export_Testsson_Kalle_001_2019-07-12_093810.csv".
8. Done

## How to run the analyser
1. Open the BloodSugarAnalyser program.
2. Select the previously exported file.
3. Click "Analyse File".
4. Done!
