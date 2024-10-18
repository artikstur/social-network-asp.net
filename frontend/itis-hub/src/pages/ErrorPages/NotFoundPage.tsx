import styled from 'styled-components';
import { Link } from 'react-router-dom';

const NotFoundPage = () => {
    return (
        <Container>
            <Title>404 - Page Not Found</Title>
            <Text>The page you are looking for does not exist.</Text>
            <LinkHome to="/">Go back</LinkHome>
        </Container>
    );
};

const Container = styled.div`
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
  background-color: #f8f9fa;
  color: #343a40;
  text-align: center;
`;

const Title = styled.h1`
    font-size: 4rem;
    margin-bottom: 1rem;
    color: #dc3545;
`;

const Text = styled.p`
    font-size: 1.5rem;
    margin-bottom: 2rem;
    color: #6c757d;
`;

const LinkHome = styled(Link)`
    font-size: 1.2rem;
    color: #007bff;
    text-decoration: none;
    &:hover {
        text-decoration: underline;
    }
`;

export default NotFoundPage;
