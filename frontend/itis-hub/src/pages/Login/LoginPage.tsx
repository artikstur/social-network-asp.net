import styled from "styled-components";
import { SubmitErrorHandler, SubmitHandler, useForm } from "react-hook-form";
import { Link } from "react-router-dom";

interface RegisterForm {
    email: string;
    password: string;
}

const LoginPage = () => {
    const { register, handleSubmit, reset, formState: { errors } } = useForm<RegisterForm>({
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

    const submit: SubmitHandler<RegisterForm> = data => {
        console.log(data);
    };

    const error: SubmitErrorHandler<RegisterForm> = data => {
        console.log(data);
    };

    return (
        <>
            <Content>
                <LogoContainer>
                    <LogoPartOne>11-307</LogoPartOne>
                    <LogoPartTwo>Hub</LogoPartTwo>
                </LogoContainer>
                <FormContainer>
                    <LoginHeader><h1>Войдите</h1></LoginHeader>
                    <Form onSubmit={handleSubmit(submit, error)}>
                        <InputContainer>
                            <Input
                                type="text"
                                {...register('email', { required: 'Email обязателен', validate: isEmail })}
                                aria-invalid={errors.email ? true : false}
                                hasError={errors.email}
                                placeholder="Введите email"
                            />
                            {<ErrorMessage isVisible={!!errors.email}>{errors.email?.message}</ErrorMessage>}
                        </InputContainer>

                        <InputContainer>
                            <Input
                                type="password"
                                {...register('password', { required: 'Пароль обязателен', validate: isPassword })}
                                aria-invalid={errors.password ? true : false}
                                hasError={errors.password}
                                placeholder="Введите пароль"
                            />
                            {<ErrorMessage isVisible={!!errors.password}>{errors.password?.message}</ErrorMessage>}

                        </InputContainer>

                        <ButtonContainer>
                            <Button type="submit">Отправить</Button>
                            <Button type="button" onClick={() => reset({ password: '' })}>Сбросить</Button>
                        </ButtonContainer>
                    </Form>
                    <LoginHeader>Не зарегистрированы? <Link to="/register">Приесоединяйтесь прямо сейчас!</Link></LoginHeader>
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

  @media (max-width: 760px) {
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
  border: ${(props) => (props.hasError ? '2px solid red' : '2px solid #ccc')};
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
  visibility: ${({isVisible}) => {
    return (isVisible ? 'visible' : 'hidden');
  }}; /* Контроль видимости */
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

export default LoginPage;