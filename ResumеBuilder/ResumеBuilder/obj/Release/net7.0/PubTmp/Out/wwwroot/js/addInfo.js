var skillGroup = document.getElementById("skillGroup");

function AddSkillField() {
    const newSkillField = document.createElement("input");

    newSkillField.type = "text";

    newSkillField.name = "skillField";

    newSkillField.classList.add("form-control");

    newSkillField.classList.add("skill-field");

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