# VisuMeuble readme

## Project Highlights
VisuMeuble is a proof-of-concept augmented reality (AR) app for Android platforms, created with the Unity engine. VisuMeuble was developed as part of Event Orizon's "Concepteur et Développeur d'Applications en Réalité Augmenté et Réalité Virtuelle" ("Augmented Reality and Virtual Reality Application Designer and Developer") training program.

VisuMeuble presents the user with a fictional furniture catalog, and leverages the Vuforia AR SDK to allow any chosen furniture pieces to be placed within an AR environment, visible through the user's phone camera.

VisuMeuble uses a 2D image target to determine where virtual furniture will appear. For this to work, the user needs to download and print a purpose-made .PNG file, which must be kept within view of the user's phone camera.

## App Features

### Store Mode
In Store Mode, the user can browse a catalog of fictional furniture and access various features such as browsing the catalog by category (tables, chairs, lamps), searching the catalog for furniture matching a specific name, and viewing detailed information about a piece of furniture. Each piece of furniture is associated with a 3D model, which can be viewed and placed in AR Mode.

### AR Mode
The app's central feature is its AR Mode. Once granted access to the user's phone camera, VisuMeuble will display the user's environment. After pointing the camera at the required image target printout, a 3D plane will appear, superimposed on the real world. The user may place furniture from Store Mode on this plane, and select it to view detailed information.

## Media
<details>
<summary>Show .gifs</summary>
![visumeuble store main page](https://github.com/user-attachments/assets/1eba24f5-e2bd-4124-9642-b52b926ec70c)
![visumeuble browsing](https://github.com/user-attachments/assets/861addc8-ed71-4594-bc95-2c8529bf21ae)
</details>

## Tools and Techniques Used
VisuMeuble makes use of the following tools and design architecture:
- Vuforia AR SDK: VisuMeuble relies on Vuforia's image target technology to detect and track the image on which virtual furniture models are superimposed.
- LeanTouch: VisuMeuble leverages the LeanTouch package's tried-and-true capability to detect touch inputs, detect drag inputs, and track selectable objects.
- ScriptableObject databases: The furniture and product categories available in the application are all stored in ScriptableObject assets, which serve as data containers. Thanks to this feature, a designer can easily modify, add or remove furniture or product categories via Unity without writing a single line of code.
- Adaptative UI: Unity's UI Toolkit was purposefully eschewed for this project, in favor of the less potent, but more stable and better-documented UGUI. Through the use of Layout and Layout Element components, VisuMeuble can successfully adapt to most phone and tablet resolutions (though it is limited to landscape only for Store Mode, and portrait only for AR Mode).
- 3D Previews: VisuMeuble uses RenderTextures to spruce up UI elements. Any UI element associated with a piece of furniture will display a rotating previews of that furniture's 3D model.
- DOTween: VisuMeuble makes use of the DOTween package in order to provide snappy, pleasing visual feedback whenever an UI button or toggle is selected.
- Design Brief: VisuMeuble was created following the guidelines and preparatory research outlined in a short, but thorough design brief. This design brief makes use of industry standard conception tools, and includes a use case diagram, a workflow diagram, UI wireframes created in Figma, a concise outline of the app's tech stack, a project milestones and delivery timeline, and development budget estimates.

## Known Issues
- AR Mode causes a black screen on Samsung Galaxy A10 and A13, and possibly other phone models.
- Several features are incomplete or missing.

## Postmortem: Retrospective on Issues and Challenges Encountered
As a training project, I value VisuMeuble as a powerful learning experience more than anything else. The app's development has taught me lessons that will guide me going forwards. As I value self-knowledge and self-improvement, I have outlined these takeaways below.

### Tackling the Virtual Elephant in the Room
Due to my lack of access to phone devices capable of running AR apps, VisuMeuble’s AR capabilities could not be tested on a real device.
My attempts to circumvent this problem and support older devices led me as far as browsing archived copies of now-lost Vuforia developer forum threads, but were ultimately unsuccessful.
In the future, it may be wiser for me to strike an arrangement with a mobile phone store, apply for a grant, or simply take the risk of purchasing a compatible device at my own expense.

### Playing to my Strengths
My unfamiliarity with AR technology and inability to test VisuMeuble on a real device has been a source of frequent discouragement through the development process. AR Mode development took much longer than planned.
Store Mode, which played to my strengths, took shape extremely quickly and was particularly encouraging to work on, but suffered from AR Mode eating up a disproportionate amount of my development time.
By the end, VisuMeuble could pass the training course’s requirements but suffered from bugs and lacked several features I would have liked to include in a portfolio piece.
In the future, I should be willing to spend less time on “learning experiences” and focus on my existing strengths in order to build up confidence and momentum I can draw on when tackling unfamiliar problems.

### Acknowledging my Weaknesses
With AR being unfamiliar terrain for me, I originally assumed I was lagging behind due to personal failings. In truth, I sometimes refuse to acknowledge my disability, which makes it difficult for me to integrate new information and follow live courses and demonstrations. After accepting this fact, I was more willing to ask for help and request tailored, step-by-step assistance with the more difficult tasks, and made significant progress as a result. This was, all things considered, this project’s most valuable lesson.
