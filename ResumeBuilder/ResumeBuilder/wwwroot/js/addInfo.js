var skillGroup = document.getElementById("skillGroup");

function AddSkillField() {
    const newSkillField = document.createElement("input");

    newSkillField.type = "text";

    newSkillField.name = "skillField";

    newSkillField.classList.add("form-control");

    newSkillField.classList.add("skill-field");

    newSkillField.required = "true";

    const lineBreak = document.createElement("br");

    lineBreak.className = "form-skill-break";

    skillGroup.appendChild(lineBreak);
    skillGroup.appendChild(newSkillField);
}

let skillsList = document.getElementsByClassName("skill-field");

let skillBreakList = document.getElementsByClassName("form-skill-break");

function RemoveSkillField() {
    skillGroup.removeChild(skillsList[skillsList.length - 1]);
    skillGroup.removeChild(skillBreakList[skillBreakList.length - 1]);
}

var interestGroup = document.getElementById("interestGroup");

function AddInterestField() {
    const newInterestField = document.createElement("input");

    newInterestField.type = "text";

    newInterestField.name = "interestField";

    newInterestField.classList.add("form-control");

    newInterestField.classList.add("interest-field");

    newInterestField.required = "true";

    const lineBreak = document.createElement("br");

    lineBreak.className = "form-interest-break";

    interestGroup.appendChild(lineBreak);
    interestGroup.appendChild(newInterestField);
}

let interestsList = document.getElementsByClassName("interest-field");

let interestBreakList = document.getElementsByClassName("form-interest-break");

function RemoveInterestField() {
    interestGroup.removeChild(interestsList[interestsList.length - 1]);
    interestGroup.removeChild(interestBreakList[interestBreakList.length - 1]);
}

var experienceGroup = document.getElementById("experienceGroup");

function AddExperienceFields() {
    const mainItem = document.getElementById("mainExperienceSet");
    const clone = mainItem.cloneNode(true);

    experienceGroup.appendChild(clone);
}

function RemoveExperienceFields() {
    let items = document.getElementsByClassName("experienceSet");
    experienceGroup.removeChild(items[items.length - 1]);
}

var educationGroup = document.getElementById("educationGroup");

function AddEducationFields() {
    const mainItem = document.getElementById("mainEducationSet");
    const clone = mainItem.cloneNode(true);

    educationGroup.appendChild(clone);
}

function RemoveEducationFields() {
    let items = document.getElementsByClassName("educationSet");
    educationGroup.removeChild(items[items.length - 1]);
}