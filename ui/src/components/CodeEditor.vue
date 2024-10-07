<script setup lang="ts">
import { Compartment, EditorState } from "@codemirror/state"
import { EditorView, highlightActiveLine, keymap, lineNumbers } from "@codemirror/view"
import { defaultKeymap, indentWithTab } from "@codemirror/commands"
import { onMounted, watch } from "vue";
import { yaml } from "@codemirror/lang-yaml";
import { defaultHighlightStyle, syntaxHighlighting } from "@codemirror/language";

const props = defineProps({
    value: { type: String, required: true },
});

const language = new Compartment;
const customKeymap = defaultKeymap.concat([indentWithTab]);

let editorRef = null;
let editorView: EditorView;
let editorStartState = EditorState.create({
    doc: props.value,
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
    editorView = new EditorView({
        state: editorStartState,
        parent: editorRef
    })
});

watch(() => props.value, (currentValue) => {
    editorView.dispatch({
        changes: {
            from: 0,
            to: editorView.state.doc.length,
            insert: currentValue,
        }
    })
});
</script>

<template>
    <div id="editor"></div>
</template>

<style scoped lang="scss">
#editor {
    font-size: var(--small-font-size);
}
</style>