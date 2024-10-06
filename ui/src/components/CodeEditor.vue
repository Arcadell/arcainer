<script setup lang="ts">
import { Compartment, EditorState } from "@codemirror/state"
import { EditorView, highlightActiveLine, keymap, lineNumbers } from "@codemirror/view"
import { defaultKeymap, indentWithTab } from "@codemirror/commands"
import { onMounted } from "vue";
import { yaml } from "@codemirror/lang-yaml";
import { defaultHighlightStyle, syntaxHighlighting } from "@codemirror/language";

let editorRef = null;
const language = new Compartment;

const customKeymap = defaultKeymap.concat([indentWithTab]);

let editorStartState = EditorState.create({
    doc: "",
    extensions: [
        keymap.of(customKeymap),
        language.of(yaml()),

        lineNumbers(),
        syntaxHighlighting(defaultHighlightStyle, { fallback: false }),
        highlightActiveLine()
    ]
});

onMounted(() => {
    editorRef = document.getElementById('editor') as HTMLElement;
    new EditorView({
        state: editorStartState,
        parent: editorRef
    })
})
</script>

<template>
    <div id="editor"></div>
</template>

<style scoped lang="scss">
#editor {
    font-size: var(--small-font-size);
}
</style>