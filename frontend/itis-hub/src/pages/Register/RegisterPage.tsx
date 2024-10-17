import styled from "styled-components";
import {SubmitErrorHandler, SubmitHandler, useForm} from "react-hook-form";
import { Link } from "react-router-dom";
import {registerUser} from "../../services/users.ts";

interface RegisterForm {
    email: string;
    userName: string;
    password: string;
    confirmPassword: string;
}

const RegisterPage = () => {
    const { register, handleSubmit, setError, reset, formState: { errors }, watch } = useForm<RegisterForm>({
        defaultValues: {
            email: "ilyas@freaky.com"
        }
    });

    const isEmail = (value: string) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(value) || 'Введите действительный адрес электронной почты';
    };

    const isPassword = (value: string) => {
        if (value.length < 8) {
            return 'Пароль должен содержать минимум 8 символов';
        }
        const hasLetter = /[a-zA-Z]/.test(value);
        const hasNumber = /[0-9]/.test(value);
        if (!hasLetter || !hasNumber) {
            return 'Пароль должен содержать хотя бы одну букву и одну цифру';
        }
        return true;
    };

    // Валидация для повторного пароля
    const isPasswordMatch = (value: string) => {
        const password = watch('password');
        return value === password || 'Пароли не совпадают';
    };

    const isUsername = (value: string) => {
        if (value.length < 8) {
            return 'Минимум 8 символов';
        }
        const usernameRegex = /^[a-zA-Z]+$/;
        return usernameRegex.test(value) || 'Ник должен содержать только английские буквы';
    };

    const submit: SubmitHandler<RegisterForm> = async (data) => {
        try {
            const result = await registerUser({ email: data.email, password: data.password, userName: data.userName});
            console.log('Register successful', result);

        } catch (error: any) {
            console.error('Register failed');

            // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
            if (error.response && error.response.status === 404) {
                setError('password', { type: 'custom', message: 'Аккаунта с такой почтой не существует' });
            } else {
                setError('password', { type: 'custom', message: 'Неверный пароль' });
            }
        }
    };

    const error: SubmitErrorHandler<RegisterForm> = data => {
        console.log("Ошибки:", data);
    };

    return (
        <>
            <Content>
                <LogoContainer>
                    <LogoPartOne>11-307</LogoPartOne>
                    <LogoPartTwo>Hub</LogoPartTwo>
                </LogoContainer>
                <FormContainer>
                    <LoginHeader><h1>Регистрация</h1></LoginHeader>
                    {/* eslint-disable-next-line @typescript-eslint/no-misused-promises */}
                    <Form onSubmit={handleSubmit(submit, error)}>
                        {/* Поле для ввода email */}
                        <InputContainer>
                            <Input
                                type="text"
                                {...register('email', {required: 'Email обязателен', validate: isEmail})}
                                aria-invalid={errors.email ? true : false}
                                hasError={!!errors.email}
                                placeholder="Введите ваш email"
                            />
                            {<ErrorMessage isVisible={!!errors.email}>{errors.email?.message}</ErrorMessage>}
                        </InputContainer>

                        <InputContainer>
                            <Input
                                type="text"
                                {...register('userName', {required: 'Ник обязателен', validate: isUsername})}
                                aria-invalid={errors.userName ? true : false}
                                hasError={!!errors.userName}
                                placeholder="Введите ваш ник"
                            />
                            {<ErrorMessage isVisible={!!errors.userName}>{errors.userName?.message}</ErrorMessage>}
                        </InputContainer>

                        <InputContainer>
                            <Input
                                type="password"
                                {...register('password', {required: 'Пароль обязателен', validate: isPassword})}
                                aria-invalid={errors.password ? true : false}
                                hasError={!!errors.password}
                                placeholder="Введите пароль"
                            />
                            {<ErrorMessage isVisible={!!errors.password}>{errors.password?.message}</ErrorMessage>}
                        </InputContainer>

                        <InputContainer>
                            <Input
                                type="password"
                                {...register('confirmPassword', {
                                    required: 'Подтверждение пароля обязательно',
                                    validate: isPasswordMatch
                                })}
                                aria-invalid={errors.confirmPassword ? true : false}
                                hasError={!!errors.confirmPassword}
                                placeholder="Повторите пароль"
                            />
                            {<ErrorMessage
                                isVisible={!!errors.confirmPassword}>{errors.confirmPassword?.message}</ErrorMessage>}
                        </InputContainer>

                        {/* Кнопки */}
                        <ButtonContainer>
                            <Button type="submit">Отправить</Button>
                            <Button type="button"
                                    onClick={() => reset({password: '', confirmPassword: ''})}>Сбросить</Button>
                        </ButtonContainer>
                    </Form>
                    <LoginHeader>
                        Уже зарегистрированы? <Link to="/login">Войдите прямо сейчас!</Link>
                    </LoginHeader>
                </FormContainer>
            </Content>
        </>
    );
};

const LoginHeader = styled.div`
  width: 100%;
  text-align: center;
  margin-bottom: 20px;
  color: #007BFF; /* Цвет фона */
  padding: 20px;
  border-radius: 8px;
  word-wrap: break-word;

  h1 {
    font-size: 33px;
    margin: 0;
  }
`;
const Content = styled.div`
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
`;

const LogoContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
  width: 400px;
  height: 100px;
  font-size: 42px;
`;

const LogoPartOne = styled.div`
  font-size: ${(props) => props.theme.fonts.logoSize}; /* Размер шрифта из темы */
  font-weight: 800;
  color: ${(props) => props.theme.colors.primary}; /* Цвет из темы */
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
`;

const LogoPartTwo = styled.div`
  font-size: ${(props) => props.theme.fonts.logoSize};
  font-weight: 800;
  color: ${(props) => props.theme.colors.text};  /* Основной цвет текста из темы */
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
`;

const FormContainer = styled.div`
  justify-content: center;
  align-items: center;
  display: flex;
  flex-direction: column;
  min-width: 290px;
  height: 100%;

  @media (max-width: 720px) {
    justify-content: flex-start;
    padding-top: 10px;
  }
`;

const Form = styled.form`
  flex-direction: column;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  padding: 10px;
`;

const InputContainer = styled.div`
  margin-bottom: 16px;
  width: 100%;
`;

const Input = styled.input<{ hasError?: boolean }>`
  width: 100%;
  padding: 10px;
  border: ${({hasError}) => (hasError ? '2px solid red' : '2px solid #ccc')};
  border-radius: 4px;
  font-size: 16px;
  outline: none;
  transition: border 0.3s ease;

  &:focus {
    border-color: ${(props) => (props.hasError ? 'red' : '#007BFF')};
  }
`;

const ErrorMessage = styled.span<{ isVisible: boolean }>`
  width: 290px;
  color: red;
  font-size: 12px;
  margin-top: 4px;
  display: block;
  min-height: 18px; /* Зарезервировать высоту */
  visibility: ${({isVisible}) => (isVisible ? 'visible' : 'hidden')}; /* Контроль видимости */
  opacity: ${(props) => (props.isVisible ? 1 : 0)};
  transition: opacity 0.3s ease-in-out;
  word-wrap: break-word; /* Перенос длинных слов */
  overflow-wrap: break-word; /* Дополнительная поддержка для переноса */
`;

const ButtonContainer = styled.div`
  display: flex;
  justify-content: space-between;
  width: 100%;
`;

const Button = styled.button`
  background-color: #007BFF;
  color: white;
  padding: 10px 20px;
  margin: 10px 0;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  width: 48%; /* Кнопки будут занимать по 48% ширины, чтобы влезли рядом */

  &:hover {
    background-color: #0056b3;
  }
`;

export default RegisterPage;
