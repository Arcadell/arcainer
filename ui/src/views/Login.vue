<script setup lang="ts">
import { useField, useForm } from 'vee-validate';
import { toTypedSchema } from '@vee-validate/zod';
import * as zod from 'zod';
import { ref } from 'vue';

const isLogin = ref(true);

const loginValidationSchema = toTypedSchema(
  zod.object({
    email: zod.string().min(1, { message: 'This is required' }).email({ message: 'Must be a valid email' }),
    password: zod.string().min(1, { message: 'This is required' }).min(8, { message: 'Too short' }),
  })
);

const registerValidationSchema = toTypedSchema(
  zod.object({
    email: zod.string().min(1, { message: 'This is required' }).email({ message: 'Must be a valid email' }),
    password: zod.string().min(1, { message: 'This is required' }).min(8, { message: 'Too short' }),
    confirmPassword: zod.string().min(1, { message: 'This is required' }).min(8, { message: 'Too short' }),
  }).superRefine(({ confirmPassword, password }, ctx) => {
    if (confirmPassword !== password) {
      ctx.addIssue({
        code: 'custom',
        message: "The passwords don't match",
        path: ['confirmPassword'],
      })
    }
  })
);

const loginForm = useForm({
  validationSchema: loginValidationSchema,
});

const registerForm = useForm({
  validationSchema: registerValidationSchema,
});

const { value: loginEmail } = useField('email', loginValidationSchema, { form: loginForm });
const { value: loginPassword } = useField('password', loginValidationSchema, { form: loginForm });

const { value: registerEmail } = useField('email', registerValidationSchema, { form: registerForm });
const { value: registerPassword } = useField('password', registerValidationSchema, { form: registerForm });
const { value: registerConfirmPassword } = useField('confirmPassword', registerValidationSchema, { form: registerForm });

const onLoginSubmit = loginForm.handleSubmit(values => {
  alert(JSON.stringify(values, null, 2));
})

const onRegisterSubmit = registerForm.handleSubmit(values => {
  alert(JSON.stringify(values, null, 2));
})

function switchToisLogin() {
  isLogin.value = !isLogin.value;
}

</script>

<template>
  <div class="main">
    <div class="left-box"></div>

    <div class="rigth-box">
      <div class="mini-box">
        <template v-if="isLogin">
          <h2>Login</h2>
          <p>Enter your email and password</p>
          <form @submit="onLoginSubmit">
            <input name="loginEmail" v-model="loginEmail" type="email" placeholder="Email" />
            <span>{{ loginForm.errors.value.email }}</span>
            <input name="loginPassword" v-model="loginPassword" type="password" placeholder="Password" />
            <span>{{ loginForm.errors.value.password }}</span>
            <button>Login</button>
          </form>
          <div class="divider">Don't have an account</div>
          <button @click="switchToisLogin" class="btn-outline">Register</button>
        </template>

        <template v-else>
          <h2>Create account</h2>
          <p>Enter your email and password</p>
          <form @submit="onRegisterSubmit">
            <input name="email" v-model="registerEmail" type="email" placeholder="Email" />
            <span>{{ registerForm.errors.value.email }}</span>
            <input name="password" v-model="registerPassword" type="password" placeholder="Password" />
            <span>{{ registerForm.errors.value.password }}</span>
            <input name="password" v-model="registerConfirmPassword" type="password" placeholder="Confirm password" />
            <span>{{ registerForm.errors.value.confirmPassword }}</span>
            <button>Create account</button>
          </form>
          <div class="divider">If you have an account</div>
          <button @click="switchToisLogin" class="btn-outline">Login</button>
        </template>
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.main {
  display: flex;

  width: 100dvw;
  height: 100dvh;
}

.left-box {
  background-color: black;
  width: 100%;
}

.rigth-box {
  display: flex;
  flex-direction: column;

  justify-content: center;
  align-items: center;

  width: 100%;
}

.mini-box {
  display: flex;
  flex-direction: column;

  justify-content: center;
  text-align: center;
  gap: 0.5rem;

  width: 300px;

  p {
    opacity: 0.5;
    font-size: 0.95rem;
  }

  form {
    margin-top: 1rem;
    display: flex;
    flex-direction: column;

    gap: 0.2rem;
  }

  .divider {
    margin: 0.5rem 0;
  }
}
</style>