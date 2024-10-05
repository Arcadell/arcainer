<script setup lang="ts">
import { EditorState } from "@codemirror/state"
import { EditorView, keymap, lineNumbers } from "@codemirror/view"
import { defaultKeymap } from "@codemirror/commands"
import { onMounted } from "vue";

let editorRef = null;
let editorView: EditorView;

let editorStartState = EditorState.create({
    doc: "",
    extensions: [
        lineNumbers(),
        keymap.of(defaultKeymap)
    ]
});

onMounted(() => {
    editorRef = document.getElementById('editor') as HTMLElement;
    editorView = new EditorView({
        state: editorStartState,
        parent: editorRef,
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